using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.Utilities.Extensions;
using ToDoList.Domain.Shared.Utilities.Validators;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjectsConstraints;

namespace ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

public record Title
{
    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Title, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Title));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                input,
                TitleConstraints.MinLength,
                TitleConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(Title));

        return new Title(input);
    }
}