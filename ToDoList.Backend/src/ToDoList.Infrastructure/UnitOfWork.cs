using Microsoft.EntityFrameworkCore.Storage;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Infrastructure.DbContexts;

namespace ToDoList.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoListWriteDbContext _dbContext;

    public UnitOfWork(ToDoListWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}