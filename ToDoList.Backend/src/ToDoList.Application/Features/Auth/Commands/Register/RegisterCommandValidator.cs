using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(rc => rc.Username)
            .MustBeValueObject(Username.Create);

        RuleFor(rc => rc.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);

        RuleFor(rc => rc.Password)
            .MustBeValueObject(Password.Create);
    }
}