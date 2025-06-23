using Ardalis.Specification;
using Fly.CleanArchitecture.Sample.Domain.Orders.Aggregates;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Specs;

public class OrderByCustomerOrderNo : Specification<Order>
{
    public OrderByCustomerOrderNo(string customerOrderNo)
    {
        Query.Where(x => x.CustomerOrderNo == customerOrderNo);
    }
}