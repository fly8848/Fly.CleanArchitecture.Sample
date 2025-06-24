using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.EventHandlers;

public class PushBSystemEventHandler : INotificationHandler<PushSystemEvent>
{
    private readonly IOrderRepository _orderRepository;

    public PushBSystemEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(PushSystemEvent notification, CancellationToken cancellationToken)
    {
        // 规范和性能需要做取舍, 如果需要性能可以在事件在传递一下 CustomerName
        var order = await _orderRepository.FindAsync(notification.OrderId, cancellationToken);
        if (order == null || order.CustomerName != "B")
        {
        }
    }
}