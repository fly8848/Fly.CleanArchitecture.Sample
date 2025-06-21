namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasDomainEvent
{
    public IReadOnlyList<DomainEvent> DomainEvents { get; }
    void AddDomainEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}