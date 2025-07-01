namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasUpdated
{
    DateTime? UpdatedTime { get; set; }
    string? UpdatedBy { get; set; }
}