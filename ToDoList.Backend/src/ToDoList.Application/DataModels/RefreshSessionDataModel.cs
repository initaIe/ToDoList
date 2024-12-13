namespace ToDoList.Application.DataModels;

public class RefreshSessionDataModel
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid Jti { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
    public Guid UserId { get; init; }
}