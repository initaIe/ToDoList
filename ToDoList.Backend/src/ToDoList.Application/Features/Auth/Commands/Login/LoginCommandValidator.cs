using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(rc => rc.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);

        RuleFor(rc => rc.Password)
            .MustBeValueObject(Password.Create);
    }
}