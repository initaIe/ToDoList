namespace ToDoList.Contracts.Requests;

public record RefreshTokensRequest(
    string AccessToken,
    Guid RefreshToken);