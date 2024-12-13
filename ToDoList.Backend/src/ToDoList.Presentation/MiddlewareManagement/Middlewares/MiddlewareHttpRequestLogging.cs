namespace ToDoList.Presentation.MiddlewareManagement.Middlewares;

public class MiddlewareHttpRequestLogging
{
    private readonly ILogger<MiddlewareHttpRequestLogging> _logger;
    private readonly RequestDelegate _next;

    public MiddlewareHttpRequestLogging(
        RequestDelegate next,
        ILogger<MiddlewareHttpRequestLogging> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation(
            "HTTP request: method -> {method}; url -> {url} | headers -> {headers} | content-type -> {contentType}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            httpContext.Request.Headers,
            httpContext.Request.ContentType);

        await _next(httpContext);

        _logger.LogInformation(
            "Response: status-code -> {statusCode}",
            httpContext.Response.StatusCode);
    }
}