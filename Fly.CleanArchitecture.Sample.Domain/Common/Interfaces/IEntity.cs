namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IEntity<T>: IEntity
{
    T Id { get; init; }
}

public interface IEntity
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}