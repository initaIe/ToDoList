using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class UsernameConstraints
{
    public const int MinLength = LengthConstraints.Min.Three;
    public const int MaxLength = LengthConstraints.Max.ExtraShort;
}