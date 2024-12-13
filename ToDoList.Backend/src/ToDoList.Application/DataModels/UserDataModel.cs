namespace ToDoList.Application.DataModels;

public class UserDataModel
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Username { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string EmailAddress { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? PhoneNumber { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string PasswordHash { get; init; } = null!;
    public IReadOnlyList<RefreshSessionDataModel> RefreshSessions { get; init; } = [];
}