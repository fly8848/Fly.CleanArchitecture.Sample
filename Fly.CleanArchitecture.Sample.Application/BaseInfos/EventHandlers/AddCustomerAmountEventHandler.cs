using Fly.CleanArchitecture.Sample.Domain.Orders.Events;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Application.BaseInfos.EventHandlers;

public class AddCustomerAmountEventHandler: INotificationHandler<AddCustomerAmountEvent>
{
    public Task Handle(AddCustomerAmountEvent notification, CancellationToken cancellationToken)
    {
        // 该事件简单体现下跨领域...
        return Task.CompletedTask;
    }
}