using FluentValidation;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;

namespace ToDoList.Application.Features.ToDoItems.Queries;

public class GetToDoItemByIdQueryValidator : AbstractValidator<GetToDoItemByIdQuery>
{
    public GetToDoItemByIdQueryValidator()
    {
        RuleFor(g => g.Id)
            .MustBeValueObject(ToDoItemId.Create);
    }
}