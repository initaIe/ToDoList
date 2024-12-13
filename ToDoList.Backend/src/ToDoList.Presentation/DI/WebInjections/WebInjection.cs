using ToDoList.Presentation.DI.WebInjections.Injections;

namespace ToDoList.Presentation.DI.WebInjections;

public static class WebInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddCustomSwaggerGen();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services;
    }
}