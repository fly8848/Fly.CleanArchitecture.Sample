using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract class AggregateRoot<TId> : AuditedEntity<TId>, IHasDomainEvent
{
    private readonly List<DomainEvent> _domainEvents = new();
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