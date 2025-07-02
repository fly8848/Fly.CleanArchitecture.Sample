using Fly.CleanArchitecture.Sample.Application.Orders;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.Fast.Persistence.EntityFrameworkCore;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}