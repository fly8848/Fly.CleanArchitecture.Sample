using Fly.CleanArchitecture.Sample.Domain.Orders.Events;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Aggregates;

public class Order : AggregateRoot<Guid>
{
    private readonly List<OrderDetail> _orderDetails = new();

    public Order(string customerName, string customerOrderNo)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        CustomerOrderNo = customerOrderNo;
        AddDomainEvent(new GenerateOrderNoEvent(Id));
    }

    public string CustomerName { get; private set; }
    public string CustomerOrderNo { get; private set; }
    public string? OrderNo { get; private set; }
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    public void SetOrderNo(string orderNo)
    {
        OrderNo = orderNo;
        AddDomainEvent(new PushSystemEvent(Id));
    }

    public void AddDetail(decimal amount, Currency currency, string name, int qty)
    {
        var money = new Money(amount, currency);
        var orderDetail = new OrderDetail(name, qty, money);
        _orderDetails.Add(orderDetail);
    }
}