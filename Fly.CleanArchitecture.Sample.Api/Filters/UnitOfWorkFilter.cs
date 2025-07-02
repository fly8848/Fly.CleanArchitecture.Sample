using System.Reflection;
using Fly.CleanArchitecture.Sample.Api.Attributes;
using Fly.Fast.Persistence.Contracts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fly.CleanArchitecture.Sample.Api.Filters;

public class UnitOfWorkFilter : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsEnabled(context))
        {
            await next();
            return;
        }

        var cancellationToken = context.HttpContext.RequestAborted;
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var resultContext = await next();
        if (resultContext.Exception == null)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken: cancellationToken);
        }
        else
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
        }
    }

    private bool IsEnabled(ActionExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor descriptor) return false;

        var methodAttribute = descriptor.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
        if (methodAttribute != null) return methodAttribute.IsEnabled;

        var controllerAttribute = descriptor.ControllerTypeInfo.GetCustomAttribute<UnitOfWorkAttribute>();
        if (controllerAttribute != null) return controllerAttribute.IsEnabled;

        return false;
    }
}