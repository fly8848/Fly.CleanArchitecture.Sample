using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract class AuditedEntity<TId> : Entity<TId>, IHasCreatedAudited, IHasUpdatedAudited, IHasDeletedAudited,
    IHasVersion
{
    public DateTime CreatedTime { get; } = default;
    public string? CreatedBy { get; } = null;
    public bool IsDeleted { get; } = false;
    public DateTime? DeletedTime { get; } = null;
    public string? DeletedBy { get; } = null;
    public DateTime? UpdatedTime { get; } = null;
    public string? UpdatedBy { get; } = null;
    public int Version { get; } = 0;
}