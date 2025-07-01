using System.Reflection;
using Fly.Fast.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator,
        ILogger<ApplicationDbContext> logger) : base(options)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateAuditedEntities();

        var domainEvents = GetAndClearDomainEvents();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchDomainEvents(domainEvents, cancellationToken);

        return result;
    }

    private void UpdateAuditedEntities()
    {
        // TODO: JWT
        var username = string.Empty;
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added && entry.Entity is IHasCreated)
            {
                entry.Property(nameof(IHasCreated.CreatedTime)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IHasCreated.CreatedBy)).CurrentValue = username;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is IHasUpdated)
                {
                    entry.Property(nameof(IHasUpdated.UpdatedTime)).CurrentValue = DateTime.UtcNow;
                    entry.Property(nameof(IHasUpdated.UpdatedBy)).CurrentValue = username;
                }

                if (entry.Entity is IHasVersion versionedEntity)
                    entry.Property(nameof(IHasVersion.Version)).CurrentValue = versionedEntity.Version + 1;
            }

            if (entry.State == EntityState.Deleted && entry.Entity is IHasDeleted)
            {
                entry.State = EntityState.Modified;
                entry.Property(nameof(IHasDeleted.DeletedTime)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IHasDeleted.DeletedBy)).CurrentValue = username;
                entry.Property(nameof(IHasDeleted.IsDeleted)).CurrentValue = true;
            }
        }
    }

    private List<DomainEvent> GetAndClearDomainEvents()
    {
        var domainEntities = ChangeTracker.Entries<IEntity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

        foreach (var domainEntity in domainEntities) domainEntity.ClearDomainEvents();

        return domainEvents;
    }

    private async Task DispatchDomainEvents(List<DomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("domainEvent: {Name}", domainEvent.GetType().Name);
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}