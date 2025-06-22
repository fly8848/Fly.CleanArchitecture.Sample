using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.EventHandlers;

public class PushASystemEventHandler : INotificationHandler<PushSystemEvent>
{
    private readonly IOrderRepository _orderRepository;

    public PushASystemEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(PushSystemEvent notification, CancellationToken cancellationToken)
    {
        // 规范和性能需要做取舍, 如果需要性能可以在事件在传递一下 CustomerName
        var order = await _orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);
        if (order == null || order.CustomerName != "A")
        {
        }
    }
}