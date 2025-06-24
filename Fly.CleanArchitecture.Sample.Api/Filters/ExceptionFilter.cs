using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fly.CleanArchitecture.Sample.Api.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.ExceptionHandled) return Task.CompletedTask;

        _logger.LogError(context.Exception, "An unhandled exception has occurred.");

        // TODO: 区分异常类型 区分环境返回错误信息
        context.Result = new ObjectResult(context.Exception.Message)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;

        return Task.CompletedTask;
    }
}