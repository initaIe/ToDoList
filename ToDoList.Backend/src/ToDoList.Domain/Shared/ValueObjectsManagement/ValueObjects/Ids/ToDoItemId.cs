using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;

namespace ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;

public record ToDoItemId
{
    private ToDoItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ToDoItemId CreateRandom()
    {
        return new ToDoItemId(Guid.NewGuid());
    }

    public static Result<ToDoItemId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new ToDoItemId(value);
    }
}