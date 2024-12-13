using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Domain.UsersManagement.Entities;

public class RefreshSession : Entity<RefreshSessionId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RefreshSession(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        RefreshSessionId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private RefreshSession(
        RefreshSessionId id,
        CreatedAt createdAt,
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
        : base(id, createdAt)
    {
        Jti = jti;
        ExpiresAt = expiresAt;
    }

    public Jti Jti { get; }
    public RefreshSessionExpiresAt ExpiresAt { get; }

    public static RefreshSession CreateNew(
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
    {
        var id = RefreshSessionId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new RefreshSession(
            id,
            createdAt,
            jti,
            expiresAt);
    }

    public static RefreshSession Create(
        RefreshSessionId id,
        CreatedAt createdAt,
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
    {
        return new RefreshSession(
            id,
            createdAt,
            jti,
            expiresAt);
    }
}