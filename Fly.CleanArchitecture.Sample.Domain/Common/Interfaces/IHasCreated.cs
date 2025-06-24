namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasCreated
{
    public DateTime CreatedTime { get; }
    public string? CreatedBy { get; }
}