using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Validators;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record Password
{
    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Password));

        if (!StringValidator.IsInRange(
                input,
                PasswordConstraints.MinLength,
                PasswordConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(Password));

        return new Password(input);
    }
}