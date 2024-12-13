using ToDoList.Domain.ToDoItemManagement.AggregateRoot;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

namespace ToDoList.Application.Factories;

public static class ToDoItemFactory
{
    public static ToDoItem ForceCreateNewToDoItem(string title)
    {
        var titleVo = Title.Create(title).Value;
        return ToDoItem.CreateNew(titleVo);
    }
}