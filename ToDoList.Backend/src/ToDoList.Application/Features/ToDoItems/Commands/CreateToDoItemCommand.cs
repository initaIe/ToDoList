using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.ToDoItems.Commands;

public record CreateToDoItemCommand(string Title) : ICommand;