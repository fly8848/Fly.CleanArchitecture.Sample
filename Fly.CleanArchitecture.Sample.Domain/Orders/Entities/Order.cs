using Fly.CleanArchitecture.Sample.Domain.Common;
using Fly.CleanArchitecture.Sample.Domain.Orders.Dtos;
using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using Fly.CleanArchitecture.Sample.Domain.Orders.ValueObjects;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

public class Order : AggregateRoot<Guid>
{
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
    
    private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    public void SetOrderNo(string orderNo)
    {
        OrderNo = orderNo;
        AddDomainEvent(new PushSystemEvent(Id));
    }

    public void AddDetails(List<OrderDetailInputDto> orderDetails)
    {
        foreach (var item in orderDetails)
        {
            var money = new Money(item.Amount, item.Currency);
            var orderDetail = new OrderDetail(item.Name, item.Qty, money);
            _orderDetails.Add(orderDetail);
        }
        
        var amount = orderDetails.Sum(x => x.Amount);
        AddDomainEvent(new AddCustomerAmountEvent(Id, amount));
    }
}