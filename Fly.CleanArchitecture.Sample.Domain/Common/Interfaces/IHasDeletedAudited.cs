namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasDeletedAudited
{
    public bool IsDeleted { get; }
    public DateTime? DeletedTime { get; }
    public string? DeletedBy { get; }
}