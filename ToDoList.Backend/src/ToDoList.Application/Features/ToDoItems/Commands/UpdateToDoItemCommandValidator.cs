using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

namespace ToDoList.Application.Features.ToDoItems.Commands;

public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(u => u.Id)
            .MustBeValueObject(ToDoItemId.Create);

        RuleFor(u => u.Title)
            .MustBeValueObject(Title.Create);
    }
}