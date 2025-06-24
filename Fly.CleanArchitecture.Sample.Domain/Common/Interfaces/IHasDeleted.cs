namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasDeleted
{
    public bool IsDeleted { get; }
    public DateTime? DeletedTime { get; }
    public string? DeletedBy { get; }
}