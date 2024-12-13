﻿using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class PhoneNumberConstraints
{
    public const int MinLength = LengthConstraints.Min.Six;
    public const int MaxLength = LengthConstraints.Max.Fifteen;
}