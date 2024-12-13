using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Abstractions.Providers;
using ToDoList.Infrastructure.Providers;

namespace ToDoList.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddRefreshSessionOptionsProvider();
        services.AddPasswordHashProvider();
        services.AddTokenProvider();

        return services;
    }

    private static IServiceCollection AddPasswordHashProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IPasswordHashProvider, PasswordHashProvider>();
    }

    private static IServiceCollection AddTokenProvider(this IServiceCollection services)
    {
        return services.AddSingleton<ITokenProvider, TokenProvider>();
    }

    private static IServiceCollection AddRefreshSessionOptionsProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IRefreshSessionOptionsProvider, RefreshSessionOptionsProvider>();
    }
}