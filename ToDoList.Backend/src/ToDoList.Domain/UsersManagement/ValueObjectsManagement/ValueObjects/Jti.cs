using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record Jti
{
    private Jti(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static Jti CreateRandom()
    {
        return new Jti(Guid.NewGuid());
    }

    public static Result<Jti, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return Errors.General.ValueIsInvalid(nameof(Jti));

        return new Jti(input);
    }
}