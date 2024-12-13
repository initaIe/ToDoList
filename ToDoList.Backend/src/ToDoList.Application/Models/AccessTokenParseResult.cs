namespace ToDoList.Application.Models;

public record AccessTokenParseResult(
    Guid AccountId,
    Guid Jti);