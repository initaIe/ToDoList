using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Application.Abstractions.Database;

public interface IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
    Task<Result<TEntity, Error>> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    void Delete(TEntity entity);
}