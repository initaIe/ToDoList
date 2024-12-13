using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjectsConstraints;

public static class TitleConstraints
{
    public const int MinLength = LengthConstraints.Min.Three;
    public const int MaxLength = LengthConstraints.Max.Hundred;
}