using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.DI.Injections;

namespace ToDoList.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);
        services.AddProviders();
        services.AddDataBase();
        services.AddAuth(configuration);

        return services;
    }
}