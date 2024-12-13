namespace ToDoList.Contracts.Requests;

public record LoginRequest(
    string EmailAddress,
    string Password);