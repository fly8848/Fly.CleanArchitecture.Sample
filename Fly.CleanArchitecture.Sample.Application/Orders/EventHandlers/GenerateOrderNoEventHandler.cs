using Fly.CleanArchitecture.Sample.Application.Common.Interfaces;
using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.EventHandlers;

public class GenerateOrderNoEventHandler : INotificationHandler<GenerateOrderNoEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateOrderNoEventHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(GenerateOrderNoEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);
        // 该校验可做可不做
        if (order == null || order.OrderNo != null) return;

        // mock流水号
        var orderNo = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString("N");

        order.SetOrderNo(orderNo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}