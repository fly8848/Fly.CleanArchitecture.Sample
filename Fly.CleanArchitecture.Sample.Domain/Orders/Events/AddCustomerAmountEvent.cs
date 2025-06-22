using Fly.CleanArchitecture.Sample.Domain.Common;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Events;

public record AddCustomerAmountEvent(Guid OrderId, decimal Amount) : DomainEvent;