using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Abstractions.Services;
using ToDoList.Application.Features.ToDoItems;

namespace ToDoList.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoItemService, ToDoItemService>();

        return services;
    }
}