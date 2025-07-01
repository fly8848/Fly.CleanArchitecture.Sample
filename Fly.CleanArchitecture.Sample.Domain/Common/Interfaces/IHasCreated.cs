namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasCreated
{
    DateTime CreatedTime { get; set; }
    string? CreatedBy { get; set; }
}