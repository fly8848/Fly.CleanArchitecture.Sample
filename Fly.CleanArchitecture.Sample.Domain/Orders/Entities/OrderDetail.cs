using Fly.CleanArchitecture.Sample.Domain.Common;
using Fly.CleanArchitecture.Sample.Domain.Orders.ValueObjects;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

public class OrderDetail : Entity<Guid>
{
    private OrderDetail()
    {
    }

    internal OrderDetail(
        string name,
        int qty,
        Money money)
    {
        Id = Guid.NewGuid();
        Name = name;
        Qty = qty;
        Money = money;

        if (qty < 0)
        {
            throw new DomainException("件数不能为负数");
        }
    }

    public string Name { get; private set; } = null!;
    public int Qty { get; private set; }
    public Money Money { get; private set; } = null!;
}