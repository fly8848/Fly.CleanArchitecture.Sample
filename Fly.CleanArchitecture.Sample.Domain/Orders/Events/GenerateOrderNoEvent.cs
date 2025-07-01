using Fly.Fast.Domain;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Events;

public record GenerateOrderNoEvent(int OrderId) : DomainEvent;