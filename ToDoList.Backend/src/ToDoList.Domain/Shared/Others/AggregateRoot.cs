using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Domain.Shared.Others;

public abstract class AggregateRoot<TId>
    : Entity<TId>
    where TId : IEquatable<TId>
{
    protected AggregateRoot(
        TId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }
}