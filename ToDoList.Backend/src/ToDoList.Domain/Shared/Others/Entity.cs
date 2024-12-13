using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Domain.Shared.Others;

public abstract class Entity<TId>
    where TId : IEquatable<TId>
{
    protected Entity(
        TId id,
        CreatedAt createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public TId Id { get; init; }
    public CreatedAt CreatedAt { get; init; }
}