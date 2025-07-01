namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IHasDeleted
{
    bool IsDeleted { get; set; }
    DateTime? DeletedTime { get; set; }
    string? DeletedBy { get; set; }
}