using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.Fast.Persistence.Contracts;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public interface IOrderRepository : IRepository<Order>
{
}

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
}