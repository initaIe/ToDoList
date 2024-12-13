using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class EmailAddressConstraints
{
    public const int MinLength = LengthConstraints.Min.Five;
    public const int MaxLength = LengthConstraints.Max.Long;
}