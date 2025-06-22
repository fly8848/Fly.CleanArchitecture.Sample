using FluentValidation;
using Fly.CleanArchitecture.Sample.Application.Orders.Commands;
using Fly.CleanArchitecture.Sample.Application.Orders.Specs;
using Fly.CleanArchitecture.Sample.Domain.Orders.Enums;

namespace Fly.CleanArchitecture.Sample.Application.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("客户必填");
        RuleFor(x => x.CustomerOrderNo)
            .NotEmpty().WithMessage("客户单号必填")
            .MustAsync(IsExistsOrder!).WithMessage("客户单号已存在");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.Name)
                .NotEmpty().WithMessage("第{CollectionIndex}行名称必填");
            item.RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("第{CollectionIndex}行金额不能为负数");
            item.RuleFor(x => x.Qty)
                .GreaterThanOrEqualTo(0).WithMessage("第{CollectionIndex}行件数不能为负数");
            item.RuleFor(x => x.Currency)
                .Must(x => Enum.IsDefined(typeof(Currency), x)).WithMessage("第{CollectionIndex}行币别有误");
        });
    }

    private async Task<bool> IsExistsOrder(string customerOrderNo, CancellationToken cancellationToken)
    {
        return !await _orderRepository.AnyAsync(new OrderByCustomerOrderNo(customerOrderNo), cancellationToken);
    }
}