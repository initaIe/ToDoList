namespace ToDoList.Contracts.Responses;

public record RefreshTokensResponse(
    string AccessToken,
    Guid RefreshToken);