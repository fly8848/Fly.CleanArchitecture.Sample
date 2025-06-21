using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract class AuditedEntity<TId> : Entity<TId>, IAuditedEntity
{
    public DateTime CreatedTime { get; private set; }
    public string? CreatedBy { get; private set; }

    void IAuditedEntity.Created(string? createdBy)
    {
        CreatedTime = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public DateTime? UpdatedTime { get; private set; }
    public string? UpdatedBy { get; private set; }

    void IAuditedEntity.Updated(string? updatedBy)
    {
        UpdatedTime = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public bool IsDeleted { get; private set; }
    public DateTime? DeletedTime { get; private set; }
    public string? DeletedBy { get; private set; }

    void IAuditedEntity.Deleted(string? deletedBy)
    {
        DeletedTime = DateTime.UtcNow;
        DeletedBy = deletedBy;
        IsDeleted = true;
    }

    public int Version { get; private set; }

    void IAuditedEntity.UpdateVersion()
    {
        Version++;
    }
}