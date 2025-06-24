namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IEntity
{
    public int Id { get; }

    public IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}