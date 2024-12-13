using ToDoList.Presentation.DI.LayersInjections.Injections;

namespace ToDoList.Presentation.DI.LayersInjections;

public static class LayersInjection
{
    public static IServiceCollection AddLayers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        return services;
    }
}