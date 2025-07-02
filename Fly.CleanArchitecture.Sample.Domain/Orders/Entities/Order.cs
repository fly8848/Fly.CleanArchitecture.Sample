using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using Fly.Fast.Domain;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

public class Order : Entity<int>, IHasCreated, IHasUpdated, IHasDeleted
{
    public Order(string customerName, string customerOrderNo)
    {
        CustomerName = customerName;
        CustomerOrderNo = customerOrderNo;
        AddDomainEvent(new GenerateOrderNoEvent(Id)); // TODO: 依赖id的事件不应该使用自增, 后续需要调整
    }

    public string CustomerName { get; private set; }
    public string CustomerOrderNo { get; private set; }
    public string? OrderNo { get; private set; }
    public string? Remark { get; set; }

    public DateTime CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string? UpdatedBy { get; set; }


    public void SetOrderNo(string orderNo)
    {
        OrderNo = orderNo;
        AddDomainEvent(new PushSystemEvent(Id));
    }
}