using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Fly.CleanArchitecture.Sample.Application.Orders;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Repositories;

public class OrderRepository: RepositoryBase<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public OrderRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
        _dbContext = dbContext;
    }
}