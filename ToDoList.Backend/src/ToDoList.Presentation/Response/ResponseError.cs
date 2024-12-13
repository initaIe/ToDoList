namespace ToDoList.Presentation.Response;

public record ResponseError(
    string Code,
    string Message,
    string? InvalidPropertyName);