using Fly.CleanArchitecture.Sample.Domain.Common;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Events;

public record GenerateOrderNoEvent(Guid OrderId): DomainEvent;