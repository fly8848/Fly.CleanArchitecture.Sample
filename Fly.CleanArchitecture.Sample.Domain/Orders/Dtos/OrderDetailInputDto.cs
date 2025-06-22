using Fly.CleanArchitecture.Sample.Domain.Orders.Enums;

namespace Fly.CleanArchitecture.Sample.Domain.Orders.Dtos;

public record OrderDetailInputDto(string Name, int Qty, decimal Amount, Currency Currency);