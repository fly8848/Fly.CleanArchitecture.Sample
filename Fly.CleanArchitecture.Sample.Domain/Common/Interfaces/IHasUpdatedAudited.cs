namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasUpdatedAudited
{
    public DateTime? UpdatedTime { get; }
    public string? UpdatedBy { get; }
}