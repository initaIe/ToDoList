using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record AccessToken
{
    private AccessToken(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<AccessToken, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(AccessToken));

        return new AccessToken(input);
    }
}