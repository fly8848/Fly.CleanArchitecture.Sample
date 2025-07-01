using Fly.CleanArchitecture.Sample.Domain.Orders.ValueObjects;
using Fly.Fast.Domain;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

public class OrderDetail : Entity<int>
{
    private OrderDetail()
    {
    }

    public OrderDetail(
        int orderId,
        string name,
        int qty,
        Money money)
    {
        OrderId = orderId;
        Name = name;
        Qty = qty;
        Money = money;

        if (qty < 0) throw new OrderException("件数不能为负数");
    }

    public int OrderId { get; private set; }
    public string Name { get; private set; } = null!;
    public int Qty { get; private set; }
    public Money Money { get; private set; } = null!;
}