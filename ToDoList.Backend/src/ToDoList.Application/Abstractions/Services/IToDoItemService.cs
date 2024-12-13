using ToDoList.Application.DataModels;
using ToDoList.Application.Features.ToDoItems.Commands;
using ToDoList.Application.Features.ToDoItems.Queries;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Application.Abstractions.Services;

public interface IToDoItemService
{
    Task<Result<Guid, ErrorList>> CreateAsync(
        CreateToDoItemCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> UpdateAsync(
        UpdateToDoItemCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<Guid, ErrorList>> DeleteAsync(
        DeleteToDoItemCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<ToDoItemDataModel, ErrorList>> GetByIdAsync(
        GetToDoItemByIdQuery query,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ToDoItemDataModel>> GetAllAsync(
        CancellationToken cancellationToken = default);
}