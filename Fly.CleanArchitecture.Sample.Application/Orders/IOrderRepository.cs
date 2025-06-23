using Ardalis.Specification;
using Fly.CleanArchitecture.Sample.Domain.Orders.Aggregates;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public interface IOrderRepository : IRepositoryBase<Order>
{
}