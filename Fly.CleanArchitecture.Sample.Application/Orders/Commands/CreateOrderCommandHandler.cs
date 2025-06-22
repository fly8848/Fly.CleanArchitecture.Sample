using AutoMapper;
using FluentValidation;
using Fly.CleanArchitecture.Sample.Application.Common.Interfaces;
using Fly.CleanArchitecture.Sample.Domain.Orders.Dtos;
using Fly.CleanArchitecture.Sample.Domain.Orders.Entities;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(
        IMapper mapper,
        IValidator<CreateOrderCommand> validator,
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }
    
    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errors = string.Join(";", result.Errors.Select(x => x.ErrorMessage));
            throw new ArgumentException(errors);
        }
        
        var detailDtos = _mapper.Map<List<OrderDetailInputDto>>(request.Items);
        var order = new Order(request.CustomerName!, request.CustomerOrderNo!);
        order.AddDetails(detailDtos);
        
        await _orderRepository.AddAsync(order, cancellationToken);
        
        return Unit.Value;
    }
}