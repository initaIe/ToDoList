﻿using FluentValidation;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Application.Validation;

public static class CustomFluentValidationValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.SerializeToString());
        });
    }
}