using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string EmailAddress,
    string Password)
    : ICommand;