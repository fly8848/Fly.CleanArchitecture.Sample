namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasCreatedAudited
{
    public DateTime CreatedTime { get; }
    public string? CreatedBy { get; }
}