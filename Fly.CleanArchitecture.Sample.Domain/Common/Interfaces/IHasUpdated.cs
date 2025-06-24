namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasUpdated
{
    public DateTime? UpdatedTime { get; }
    public string? UpdatedBy { get; }
}