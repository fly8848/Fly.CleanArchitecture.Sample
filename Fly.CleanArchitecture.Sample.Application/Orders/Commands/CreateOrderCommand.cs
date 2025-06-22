using Fly.CleanArchitecture.Sample.Application.Orders.Dtos;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Commands;

public record CreateOrderCommand(string? CustomerName, string? CustomerOrderNo, List<CreateOrderDetailInputDto> Items)
    : IRequest<Unit>;