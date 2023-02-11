namespace Ordering.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
