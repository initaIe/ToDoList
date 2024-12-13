using ToDoList.Application.Abstractions.Markers;

namespace ToDoList.Application.Features.ToDoItems.Queries;

public record GetToDoItemByIdQuery(Guid Id) : IQuery;