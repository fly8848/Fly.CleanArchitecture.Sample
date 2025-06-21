namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IAuditedEntity
{
    DateTime CreatedTime { get; }
    string? CreatedBy { get; }
    void Created(string? createdBy);

    DateTime? UpdatedTime { get; }
    string? UpdatedBy { get; }
    void Updated(string? updatedBy);

    bool IsDeleted { get; }
    DateTime? DeletedTime { get; }
    string? DeletedBy { get; }
    void Deleted(string? deletedBy);

    int Version { get; } 
    void UpdateVersion();
}