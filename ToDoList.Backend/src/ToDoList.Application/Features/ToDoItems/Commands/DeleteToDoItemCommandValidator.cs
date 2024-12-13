using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;

namespace ToDoList.Application.Features.ToDoItems.Commands;

public class DeleteToDoItemCommandValidator : AbstractValidator<DeleteToDoItemCommand>
{
    public DeleteToDoItemCommandValidator()
    {
        RuleFor(d => d.Id)
            .MustBeValueObject(ToDoItemId.Create);
    }
}