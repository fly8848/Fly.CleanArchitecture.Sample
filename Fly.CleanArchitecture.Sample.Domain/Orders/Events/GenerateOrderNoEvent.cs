using Fly.CleanArchitecture.Sample.Domain.Common;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Events;

public record GenerateOrderNoEvent(int OrderId) : DomainEvent;