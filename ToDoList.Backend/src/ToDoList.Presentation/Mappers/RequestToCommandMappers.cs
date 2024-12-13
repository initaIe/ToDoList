using ToDoList.Application.Features.Auth.Commands.Login;
using ToDoList.Application.Features.Auth.Commands.RefreshTokens;
using ToDoList.Application.Features.Auth.Commands.Register;
using ToDoList.Application.Features.ToDoItems.Commands;
using ToDoList.Contracts.Requests;

namespace ToDoList.Presentation.Mappers;

public static class RequestToCommandMappers
{
    public static CreateToDoItemCommand ToCommand(this CreateToDoItemRequest request)
        => new(request.Title);

    public static UpdateToDoItemCommand ToCommand(this UpdateToDoItemRequest request, Guid id)
        => new(id, request.Title, request.IsCompleted);

    public static LoginCommand ToCommand(this LoginRequest request)
        => new(request.EmailAddress, request.Password);

    public static RegisterCommand ToCommand(this RegisterRequest request)
        => new(request.Username, request.EmailAddress, request.Password);

    public static RefreshTokensCommand ToCommand(this RefreshTokensRequest request)
        => new(request.AccessToken, request.RefreshToken);
}