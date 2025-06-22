namespace Fly.CleanArchitecture.Sample.Application.Orders.Dtos;

public record CreateOrderDetailInputDto(string? Name, int Qty, decimal Amount, int Currency);