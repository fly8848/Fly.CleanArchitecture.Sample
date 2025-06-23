using Fly.CleanArchitecture.Sample.Application.Orders.Dtos;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Commands;

public record CreateOrderCommand(string CustomerName, string CustomerOrderNo, List<CreateOrderDetailInputDto> Items)
    : IRequest<Unit>;