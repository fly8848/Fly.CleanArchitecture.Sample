using System.Reflection;
using FluentValidation;
using Fly.CleanArchitecture.Sample.Application.Orders.Commands;
using Fly.CleanArchitecture.Sample.Application.Orders.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Fly.CleanArchitecture.Sample.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
    }
}