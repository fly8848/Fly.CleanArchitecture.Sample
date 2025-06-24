using Fly.CleanArchitecture.Sample.Domain.Common;
using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;
using Fly.CleanArchitecture.Sample.Domain.Orders.Events;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

public class Order : Entity, IHasCreated, IHasUpdated, IHasDeleted
{
    public Order(string customerName, string customerOrderNo)
    {
        CustomerName = customerName;
        CustomerOrderNo = customerOrderNo;
        AddDomainEvent(new GenerateOrderNoEvent(Id));
    }

    public string CustomerName { get; private set; }
    public string CustomerOrderNo { get; private set; }
    public string? OrderNo { get; private set; }

    public DateTime CreatedTime { get; } = default;
    public string? CreatedBy { get; } = null;
    public bool IsDeleted { get; } = false;
    public DateTime? DeletedTime { get; } = null;
    public string? DeletedBy { get; } = null;
    public DateTime? UpdatedTime { get; } = null;
    public string? UpdatedBy { get; } = null;

    public void SetOrderNo(string orderNo)
    {
        OrderNo = orderNo;
        AddDomainEvent(new PushSystemEvent(Id));
    }
}