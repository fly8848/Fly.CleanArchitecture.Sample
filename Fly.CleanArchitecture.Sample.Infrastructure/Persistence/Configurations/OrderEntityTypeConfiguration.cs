using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.Fast.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Configurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.AddConfigure();
    }
}

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.AddConfigure();

        builder.ComplexProperty(x => x.Money, x =>
        {
            x.Property(y => y.Amount);
            x.Property(y => y.Currency);
        });
    }
}