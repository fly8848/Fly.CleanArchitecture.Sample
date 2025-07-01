using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using Fly.CleanArchitecture.Sample.Domain.Orders.Enums;
using Fly.CleanArchitecture.Sample.Domain.Orders.ValueObjects;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName, request.CustomerOrderNo);
        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var orderDetails = new List<OrderDetail>();
        foreach (var item in request.Items)
        {
            var money = new Money(item.Amount, (Currency)item.Currency);
            var orderDetail = new OrderDetail(order.Id, item.Name, item.Qty, money);
            orderDetails.Add(orderDetail);
        }

        await _orderDetailRepository.AddRangeAsync(orderDetails, cancellationToken);

        return Unit.Value;
    }
}