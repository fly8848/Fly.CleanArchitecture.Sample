namespace Fly.CleanArchitecture.Sample.Domain.Orders.Events;

// 由于外部系统可能不稳定, 真正来说还需要包一层本地消息表
public record PushSystemEvent(Guid OrderId) : DomainEvent;