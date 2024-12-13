using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.AggregateRoot;
using ToDoList.Infrastructure.DbContexts;

namespace ToDoList.Infrastructure.Repositories;

public class UsersRepository : IRepository<User, UserId>
{
    private readonly ToDoListWriteDbContext _dbContext;

    public UsersRepository(ToDoListWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<User, Error>> GetByIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .Include(u => u.RefreshSessions)
            .FirstOrDefaultAsync(
                user => user.Id == userId,
                cancellationToken);

        if (user == null)
            return Errors.General.RecordNotFound(
                nameof(User),
                nameof(UserId),
                userId.Value);

        return user;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }
}