using Ardalis.Specification;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public interface IOrderRepository: IRepositoryBase<Order>
{
    
}