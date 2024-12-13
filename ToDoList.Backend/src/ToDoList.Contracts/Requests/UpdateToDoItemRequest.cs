namespace ToDoList.Contracts.Requests;

public record UpdateToDoItemRequest(string Title, bool IsCompleted);