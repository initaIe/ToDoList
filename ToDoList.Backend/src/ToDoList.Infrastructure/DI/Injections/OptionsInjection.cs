using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.DI.Injections;

public static class OptionsInjection
{
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SqliteOptions>(configuration.GetRequiredSection(SqliteOptions.SectionName));
        services.Configure<JwtBearerAuthOptions>(configuration.GetRequiredSection(JwtBearerAuthOptions.SectionName));
        services.Configure<RefreshSessionOptions>(configuration.GetRequiredSection(RefreshSessionOptions.SectionName));

        return services;
    }
}