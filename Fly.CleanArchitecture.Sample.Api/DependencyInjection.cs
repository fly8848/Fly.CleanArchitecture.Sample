using Fly.CleanArchitecture.Sample.Api.Filters;

namespace Fly.CleanArchitecture.Sample.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddControllers(x =>
        {
            x.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

            x.Filters.Add<ExceptionFilter>();

            x.Filters.Add<ValidatorFilter>();
            x.Filters.Add<UnitOfWorkFilter>();

            x.Filters.Add<ResponseFilter>();
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}