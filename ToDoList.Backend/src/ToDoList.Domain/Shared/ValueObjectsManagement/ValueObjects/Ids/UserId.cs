using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;

namespace ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;

public record UserId
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserId CreateRandom()
    {
        return new UserId(Guid.NewGuid());
    }

    public static Result<UserId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new UserId(value);
    }
}