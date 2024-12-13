using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Infrastructure.DbContexts;

namespace ToDoList.Infrastructure.DI.Injections;

public static class DatabaseInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        return services
            .AddDbContexts()
            .AddUnitOfWork();
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        return services
            .AddScoped<ToDoListWriteDbContext>()
            .AddScoped<IToDoListReadDbContext, ToDoListReadDbContext>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}