using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.Auth.Commands.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken)
    : ICommand;