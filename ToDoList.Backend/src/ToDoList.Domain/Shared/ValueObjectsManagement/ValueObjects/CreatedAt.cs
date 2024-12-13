using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;

public record CreatedAt
{
    private CreatedAt(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static CreatedAt CreateNew()
        => new CreatedAt(DateTime.UtcNow);

    public static Result<CreatedAt, Error> Create(DateTime input)
    {
        if (input > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(CreatedAt));

        return new CreatedAt(input);
    }
}