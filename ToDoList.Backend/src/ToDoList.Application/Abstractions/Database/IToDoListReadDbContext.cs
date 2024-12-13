using ToDoList.Application.DataModels;

namespace ToDoList.Application.Abstractions.Database;

public interface IToDoListReadDbContext
{
    IQueryable<UserDataModel> Users { get; }
    IQueryable<ToDoItemDataModel> ToDoItems { get; }
}