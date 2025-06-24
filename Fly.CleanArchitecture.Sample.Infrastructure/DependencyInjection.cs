using Fly.CleanArchitecture.Sample.Application.Common.Interfaces;
using Fly.CleanArchitecture.Sample.Application.Orders;
using Fly.CleanArchitecture.Sample.Infrastructure.Persistence;
using Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fly.CleanArchitecture.Sample.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("MysqlConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
    }
}