using System.Net;
using System.Reflection;
using Fly.CleanArchitecture.Sample.Api.Attributes;
using Fly.CleanArchitecture.Sample.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fly.CleanArchitecture.Sample.Api.Filters;

public class ResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!IsEnabled(context))
        {
            await next();
            return;
        }

        if (context.Result is ObjectResult objectResult)
        {
            // TODO: 后续看看是否需要去掉泛型 免得类型不匹配
            if (objectResult.Value is ApiResponse<object>)
            {
                await next();
                return;
            }

            var statusCode = objectResult.StatusCode ?? (int)HttpStatusCode.OK;
            if (statusCode >= (int)HttpStatusCode.OK && statusCode < (int)HttpStatusCode.MultipleChoices)
            {
                var response = ApiResponse<object>.Success(objectResult.Value);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = statusCode
                };
            }
            else
            {
                var response = ApiResponse<object>.Fail(objectResult.Value!.ToString()!);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = statusCode
                };
            }
        }
        else if (context.Result is EmptyResult)
        {
            var result = ApiResponse<object>.Success(null);
            context.Result = new ObjectResult(result)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        await next();
    }

    private bool IsEnabled(ResultExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor descriptor)
        {
            return false;
        }

        var methodAttribute = descriptor.MethodInfo.GetCustomAttribute<ResponseAttribute>();
        if (methodAttribute != null)
        {
            return methodAttribute.IsEnabled;
        }

        var controllerAttribute = descriptor.ControllerTypeInfo.GetCustomAttribute<ResponseAttribute>();
        if (controllerAttribute != null)
        {
            return controllerAttribute.IsEnabled;
        }

        return false;
    }
}