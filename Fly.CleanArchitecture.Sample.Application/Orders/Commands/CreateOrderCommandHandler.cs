using AutoMapper;
using Fly.CleanArchitecture.Sample.Domain.Orders.Dtos;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
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
        var detailDtos = _mapper.Map<List<OrderDetailInputDto>>(request.Items);
        var order = new Order(request.CustomerName!, request.CustomerOrderNo!);
        order.AddDetails(detailDtos);

        await _orderRepository.AddAsync(order, cancellationToken);

        return Unit.Value;
    }
}