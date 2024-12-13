using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Presentation.Response;

namespace ToDoList.Presentation.MiddlewareManagement.Middlewares;

public class MiddlewareException
{
    private readonly ILogger<MiddlewareException> _logger;
    private readonly RequestDelegate _next;

    public MiddlewareException(
        RequestDelegate next,
        ILogger<MiddlewareException> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception: {ex}", ex.Message);

            var error = Error.Failure("server.internal", ex.Message);

            var envelope = Envelope.Error(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}