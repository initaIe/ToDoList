using Microsoft.EntityFrameworkCore.Storage;

namespace ToDoList.Application.Abstractions.Database;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}