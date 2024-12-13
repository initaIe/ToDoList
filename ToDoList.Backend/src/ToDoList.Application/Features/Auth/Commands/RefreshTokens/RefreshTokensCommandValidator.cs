using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Application.Features.Auth.Commands.RefreshTokens;

public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(rtc => rtc.RefreshToken)
            .MustBeValueObject(RefreshSessionId.Create);

        RuleFor(rtc => rtc.AccessToken)
            .MustBeValueObject(AccessToken.Create);
    }
}