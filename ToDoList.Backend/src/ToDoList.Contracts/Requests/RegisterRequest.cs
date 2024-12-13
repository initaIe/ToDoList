namespace ToDoList.Contracts.Requests;

public record RegisterRequest(
    string Username,
    string EmailAddress,
    string Password);