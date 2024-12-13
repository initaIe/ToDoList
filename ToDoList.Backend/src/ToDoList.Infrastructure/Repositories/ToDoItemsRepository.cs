using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.ToDoItemManagement.AggregateRoot;
using ToDoList.Infrastructure.DbContexts;

namespace ToDoList.Infrastructure.Repositories;

public class ToDoItemsRepository : IRepository<ToDoItem, ToDoItemId>
{
    private readonly ToDoListWriteDbContext _dbContext;

    public ToDoItemsRepository(ToDoListWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ToDoItem, Error>> GetByIdAsync(
        ToDoItemId toDoItemId,
        CancellationToken cancellationToken = default)
    {
        var toDoItem = await _dbContext.ToDoItems
            .FirstOrDefaultAsync(
                toDoItem => toDoItem.Id == toDoItemId,
                cancellationToken);

        if (toDoItem == null)
            return Errors.General.RecordNotFound(
                nameof(ToDoItem),
                nameof(toDoItemId),
                toDoItemId.Value);

        return toDoItem;
    }

    public async Task AddAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default)
    {
        await _dbContext.ToDoItems.AddAsync(toDoItem, cancellationToken);
    }

    public void Delete(ToDoItem toDoItem)
    {
        _dbContext.ToDoItems.Remove(toDoItem);
    }
}