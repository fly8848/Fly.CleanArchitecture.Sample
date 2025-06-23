using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Common;

public static class EntityTypeBuilderExtensions
{
    public static void AddConfigure<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        var entityType = typeof(T);
        if (entityType.IsAssignableTo(typeof(IHasCreatedAudited)))
        {
            builder.Property(e => ((IHasCreatedAudited)e).CreatedTime).IsRequired();
            builder.Property(e => ((IHasCreatedAudited)e).CreatedBy).HasMaxLength(255);
        }

        if (entityType.IsAssignableTo(typeof(IHasUpdatedAudited)))
        {
            builder.Property(e => ((IHasUpdatedAudited)e).UpdatedTime);
            builder.Property(e => ((IHasUpdatedAudited)e).UpdatedBy).HasMaxLength(255);
        }

        if (entityType.IsAssignableTo(typeof(IHasDeletedAudited)))
        {
            builder.Property(e => ((IHasDeletedAudited)e).DeletedTime);
            builder.Property(e => ((IHasDeletedAudited)e).DeletedBy).HasMaxLength(255);
            builder.Property(e => ((IHasDeletedAudited)e).IsDeleted).IsRequired();

            builder.HasQueryFilter(e => !((IHasDeletedAudited)e).IsDeleted);
        }

        if (entityType.IsAssignableTo(typeof(IHasVersion)))
            builder.Property(e => ((IHasVersion)e).Version).IsRequired().IsConcurrencyToken();

        if (entityType.IsAssignableTo(typeof(IHasDomainEvent))) builder.Ignore(e => ((IHasDomainEvent)e).DomainEvents);
    }
}