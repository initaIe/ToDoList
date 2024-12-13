using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

namespace ToDoList.Domain.ToDoItemManagement.AggregateRoot;

public class ToDoItem : AggregateRoot<ToDoItemId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ToDoItem(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        ToDoItemId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private ToDoItem(
        ToDoItemId id,
        CreatedAt createdAt,
        Title title,
        bool isCompleted)
        : base(id, createdAt)
    {
        Title = title;
        IsCompleted = isCompleted;
    }

    public Title Title { get; private set; }
    public bool IsCompleted { get; private set; }

    #region Factory methods

    public static ToDoItem CreateNew(Title title)
    {
        var id = ToDoItemId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();
        var isCompleted = false;

        return new ToDoItem(
            id,
            createdAt,
            title,
            isCompleted);
    }

    public static ToDoItem Create(
        ToDoItemId id,
        CreatedAt createdAt,
        Title title,
        bool isCompleted)
    {
        return new ToDoItem(
            id,
            createdAt,
            title,
            isCompleted);
    }

    #endregion

    #region CRUD

    public void Update(
        Title title,
        bool isCompleted)
    {
        Title = title;
        IsCompleted = isCompleted;
    }

    #endregion
}