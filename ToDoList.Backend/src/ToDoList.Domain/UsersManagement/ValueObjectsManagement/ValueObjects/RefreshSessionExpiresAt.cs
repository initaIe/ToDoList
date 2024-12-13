using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public class RefreshSessionExpiresAt
{
    private RefreshSessionExpiresAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static Result<RefreshSessionExpiresAt, Error> Create(DateTimeOffset input)
    {
        if (input < DateTimeOffset.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(RefreshSessionExpiresAt));

        return new RefreshSessionExpiresAt(input);
    }

    public static Result<RefreshSessionExpiresAt, Error> Create(int expiresInDays)
    {
        if (expiresInDays < 1)
            return Errors.General.ValueIsInvalid(nameof(RefreshSessionExpiresAt));

        var expiresAtDateTimeOffset = DateTimeOffset.UtcNow.AddDays(expiresInDays);

        return new RefreshSessionExpiresAt(expiresAtDateTimeOffset);
    }
}