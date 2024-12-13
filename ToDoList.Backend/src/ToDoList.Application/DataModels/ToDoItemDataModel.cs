namespace ToDoList.Application.DataModels;

public class ToDoItemDataModel
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Title { get; init; } = null!;
    public bool IsCompleted { get; init; }
}