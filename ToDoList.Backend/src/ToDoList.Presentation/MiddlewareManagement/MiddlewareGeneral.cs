using ToDoList.Presentation.MiddlewareManagement.Utils;

namespace ToDoList.Presentation.MiddlewareManagement;

public static class MiddlewareGeneral
{
    public static WebApplication UseAllMiddlewares(this WebApplication app)
    {
        app.UseMiddlewareException();
        app.UseMiddlewareHttpRequestLogging();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}