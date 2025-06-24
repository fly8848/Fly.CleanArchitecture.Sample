using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Common;

public static class EntityTypeBuilderExtensions
{
    public static void AddConfigure<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        var entityType = typeof(T);

        if (entityType.IsAssignableTo(typeof(IEntity)))
        {
            builder.Ignore(e => ((IEntity)e).DomainEvents);
            builder.HasKey(e => ((IEntity)e).Id);
        }

        if (entityType.IsAssignableTo(typeof(IHasCreated)))
        {
            builder.Property(e => ((IHasCreated)e).CreatedTime).IsRequired();
            builder.Property(e => ((IHasCreated)e).CreatedBy).HasMaxLength(255);
        }

        if (entityType.IsAssignableTo(typeof(IHasUpdated)))
        {
            builder.Property(e => ((IHasUpdated)e).UpdatedTime);
            builder.Property(e => ((IHasUpdated)e).UpdatedBy).HasMaxLength(255);
        }

        if (entityType.IsAssignableTo(typeof(IHasDeleted)))
        {
            builder.Property(e => ((IHasDeleted)e).DeletedTime);
            builder.Property(e => ((IHasDeleted)e).DeletedBy).HasMaxLength(255);
            builder.Property(e => ((IHasDeleted)e).IsDeleted).IsRequired();

            builder.HasQueryFilter(e => !((IHasDeleted)e).IsDeleted);
        }

        if (entityType.IsAssignableTo(typeof(IHasVersion)))
            builder.Property(e => ((IHasVersion)e).Version).IsRequired().IsConcurrencyToken();
    }
}