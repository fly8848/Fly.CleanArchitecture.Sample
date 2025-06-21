namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract class Entity<TId>
{
    public TId Id { get; init; } = default!;
}