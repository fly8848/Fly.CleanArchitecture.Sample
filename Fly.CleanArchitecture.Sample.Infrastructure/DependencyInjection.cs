using Fly.CleanArchitecture.Sample.Application.Orders;
using Fly.CleanArchitecture.Sample.Infrastructure.Persistence;
using Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Repositories;
using Fly.Fast.Domain;
using Fly.Fast.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fly.CleanArchitecture.Sample.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("MysqlConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        // TODO: 代码待抽离
        services.AddUnitOfWork<ApplicationDbContext>(options =>
        {
            options.BeforeSaveChangesAsync = BeforeSaveChangesAsync;
            options.AfterSaveChangesAsync = AfterSaveChangesAsync;
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
    }

    private static Task BeforeSaveChangesAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // TODO: JWT
        var username = string.Empty;
        var entries = dbContext.ChangeTracker.Entries().ToList();
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

        return Task.CompletedTask;
    }

    private static async Task AfterSaveChangesAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var entries = dbContext.ChangeTracker.Entries<IHasDomainEvent>().ToList();

        var domainEntities = entries.Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

        foreach (var domainEntity in domainEntities) domainEntity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents) await mediator.Publish(domainEvent);
    }
}