using AutoMapper;
using Fly.CleanArchitecture.Sample.Application.Orders.Dtos;
using Fly.CleanArchitecture.Sample.Domain.Orders.Dtos;

namespace Fly.CleanArchitecture.Sample.Application.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDetailInputDto, OrderDetailInputDto>();
    }
}