using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

namespace ToDoList.Application.Features.ToDoItems.Commands;

public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(c => c.Title)
            .MustBeValueObject(Title.Create);
    }
}