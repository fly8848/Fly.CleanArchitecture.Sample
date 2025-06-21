namespace Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

public interface IAuditedEntity
{
    DateTime CreatedTime { get; }
    string? CreatedBy { get; }

    DateTime? UpdatedTime { get; }
    string? UpdatedBy { get; }

    bool IsDeleted { get; }
    DateTime? DeletedTime { get; }
    string? DeletedBy { get; }

    int Version { get; }
    void Created(string? createdBy);
    void Updated(string? updatedBy);
    void Deleted(string? deletedBy);
    void UpdateVersion();
}