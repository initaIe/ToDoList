using ToDoList.Presentation.DI.LayersInjections;
using ToDoList.Presentation.DI.WebInjections;

namespace ToDoList.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAllDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLayers(configuration);
        services.AddWeb();

        return services;
    }
}