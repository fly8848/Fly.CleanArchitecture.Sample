using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Configurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.AddConfigure();

        builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.CustomerOrderNo).IsRequired().HasMaxLength(255);
        builder.Property(x => x.OrderNo).HasMaxLength(255);
        builder.HasMany(x => x.OrderDetails).WithOne();
    }
}

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.AddConfigure();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Qty).IsRequired();

        builder.ComplexProperty(x => x.Money, x =>
        {
            x.Property(y => y.Amount).IsRequired();
            x.Property(y => y.Currency).IsRequired();
        });
    }
}