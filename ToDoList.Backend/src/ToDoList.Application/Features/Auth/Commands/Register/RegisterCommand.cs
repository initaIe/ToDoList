using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string Username,
    string EmailAddress,
    string Password)
    : ICommand;