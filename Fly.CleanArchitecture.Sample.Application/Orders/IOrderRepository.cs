using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.Fast.Application.EFCore;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public interface IOrderRepository : IRepository<Order>
{
}

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
}