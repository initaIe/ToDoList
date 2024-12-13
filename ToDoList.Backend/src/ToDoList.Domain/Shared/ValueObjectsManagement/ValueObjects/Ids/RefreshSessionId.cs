using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;

namespace ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;

public record RefreshSessionId
{
    private RefreshSessionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RefreshSessionId CreateRandom()
    {
        return new RefreshSessionId(Guid.NewGuid());
    }

    public static Result<RefreshSessionId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new RefreshSessionId(value);
    }
}