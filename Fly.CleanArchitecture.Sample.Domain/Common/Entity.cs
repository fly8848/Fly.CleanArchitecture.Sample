using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract class Entity : IEntity
{
    private readonly List<DomainEvent> _domainEvents = new();
    public int Id { get; } = 0;
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        if (!_domainEvents.Contains(domainEvent)) _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}