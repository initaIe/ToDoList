namespace ToDoList.Application.Abstractions.Providers;

public interface IRefreshSessionOptionsProvider
{
    int GetExpireInDays();
}