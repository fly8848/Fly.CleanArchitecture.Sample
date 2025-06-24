using Fly.CleanArchitecture.Sample.Application.Common;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public interface IOrderRepository : IRepository<Order>
{
}

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    
}