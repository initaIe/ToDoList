using ToDoList.Presentation.MiddlewareManagement.Middlewares;

namespace ToDoList.Presentation.MiddlewareManagement.Utils;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewareException(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MiddlewareException>();
    }

    public static IApplicationBuilder UseMiddlewareHttpRequestLogging(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MiddlewareHttpRequestLogging>();
    }
}