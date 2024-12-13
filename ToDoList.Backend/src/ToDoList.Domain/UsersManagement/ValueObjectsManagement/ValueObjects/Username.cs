using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record Username
{
    private Username(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Username, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Username));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                UsernameConstraints.MinLength,
                UsernameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDigits(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(Username));

        return new Username(input);
    }
}