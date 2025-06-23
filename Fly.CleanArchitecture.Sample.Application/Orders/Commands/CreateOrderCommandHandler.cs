using AutoMapper;
using Fly.CleanArchitecture.Sample.Domain.Orders.Aggregates;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(
        IMapper mapper,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName!, request.CustomerOrderNo!);
        foreach (var item in request.Items) order.AddDetail(item.Amount, (Currency)item.Currency, item.Name, item.Qty);

        await _orderRepository.AddAsync(order, cancellationToken);

        return Unit.Value;
    }
}