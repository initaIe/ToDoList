using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.ToDoItems.Commands;

public record UpdateToDoItemCommand(
    Guid Id,
    string Title,
    bool IsCompleted)
    : ICommand;