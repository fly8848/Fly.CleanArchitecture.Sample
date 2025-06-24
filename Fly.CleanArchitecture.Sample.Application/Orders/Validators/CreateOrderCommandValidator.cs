using FluentValidation;
using Fly.CleanArchitecture.Sample.Application.Orders.Commands;
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
                .NotEmpty().WithMessage("名称必填");
            item.RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("金额不能为负数");
            item.RuleFor(x => x.Qty)
                .GreaterThanOrEqualTo(0).WithMessage("件数不能为负数");
            item.RuleFor(x => x.Currency)
                .Must(x => Enum.IsDefined(typeof(Currency), x)).WithMessage("币别有误");
        });
    }

    private async Task<bool> IsExistsOrder(string customerOrderNo, CancellationToken cancellationToken)
    {
        var queryable = await _orderRepository.AsQueryableAsync();
        queryable = queryable.Where(x => x.CustomerOrderNo == customerOrderNo);
        return !await _orderRepository.AnyAsync(queryable, cancellationToken);
    }
}