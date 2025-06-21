namespace Fly.CleanArchitecture.Sample.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}