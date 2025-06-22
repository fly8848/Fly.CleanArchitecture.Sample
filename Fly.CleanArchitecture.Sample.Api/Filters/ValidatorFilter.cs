using System.Reflection;
using System.Text;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Fly.CleanArchitecture.Sample.Api.Attributes;
using Fly.CleanArchitecture.Sample.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fly.CleanArchitecture.Sample.Api.Filters;

public class ValidatorFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsEnabled(context))
        {
            await next();
            return;
        }
        
        var failures = new List<ValidationFailure>();
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
            {
                continue;
            }

            var argumentType = argument.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
            {
                continue;
            }
            
            var validationContext = new ValidationContext<object>(argument);
            var validationResult = await validator.ValidateAsync(validationContext);

            if (!validationResult.IsValid)
            {
                failures.AddRange(validationResult.Errors);
            }
        }

        if (failures.Count > 0)
        {
            var errorMessage = GetErrorMessage(failures);
            context.Result = new BadRequestObjectResult(errorMessage);
            return;
        }

        await next();
    }

    private string GetErrorMessage(List<ValidationFailure> failures)
    {
        var errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());

        var errorMessage = new StringBuilder();
        foreach (var keyValuePair in errors)
        {
            errorMessage.Append($"{keyValuePair.Key}:{string.Join(",", keyValuePair.Value)};");
        }
        return errorMessage.ToString();
    }

    private bool IsEnabled(ActionExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor descriptor)
        {
            return false;
        }

        var methodAttribute = descriptor.MethodInfo.GetCustomAttribute<ValidatorAttribute>();
        if (methodAttribute != null)
        {
            return methodAttribute.IsEnabled;
        }

        var controllerAttribute = descriptor.ControllerTypeInfo.GetCustomAttribute<ValidatorAttribute>();
        if (controllerAttribute != null)
        {
            return controllerAttribute.IsEnabled;
        }

        return false;
    }
}