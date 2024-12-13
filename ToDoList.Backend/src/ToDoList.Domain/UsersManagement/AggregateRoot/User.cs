using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.Entities;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Domain.UsersManagement.AggregateRoot;

public class User : AggregateRoot<UserId>
{
    private readonly List<RefreshSession> _refreshSessions = [];

    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        UserId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private User(
        UserId id,
        CreatedAt createdAt,
        Username username,
        EmailAddress emailAddress,
        string passwordHash)
        : base(id, createdAt)
    {
        Username = username;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
    }

    public Username Username { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;

    #region Factory methods

    public static User CreateNew(
        Username username,
        EmailAddress emailAddress,
        string passwordHash)
    {
        var id = UserId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new User(
            id,
            createdAt,
            username,
            emailAddress,
            passwordHash);
    }

    public static User Create(
        UserId id,
        CreatedAt createdAt,
        Username username,
        EmailAddress emailAddress,
        string passwordHash)
    {
        return new User(
            id,
            createdAt,
            username,
            emailAddress,
            passwordHash);
    }

    #endregion

    #region RefreshSessions CRUD

    public Result<RefreshSession, Error> GetRefreshSessionById(RefreshSessionId refreshSessionId)
    {
        var refreshSession = _refreshSessions.FirstOrDefault(rs => rs.Id == refreshSessionId);

        if (refreshSession == null)
            return Errors.General.RecordNotFound(nameof(RefreshSession));

        return refreshSession;
    }

    public bool HasRefreshSession(RefreshSession refreshSession)
        => _refreshSessions.Contains(refreshSession);

    public Result<Error> AddRefreshSession(RefreshSession refreshSession)
    {
        var isRefreshSessionAlreadyExist = HasRefreshSession(refreshSession);

        if (isRefreshSessionAlreadyExist)
            return Errors.General.RecordAlreadyExist(nameof(RefreshSession));

        _refreshSessions.Add(refreshSession);
        return true;
    }

    public Result<Error> AddRefreshSessions(IEnumerable<RefreshSession> refreshSessions)
    {
        foreach (var refreshSession in refreshSessions)
        {
            var addRefreshSessionResult = AddRefreshSession(refreshSession);

            if (addRefreshSessionResult.IsFailure)
                return addRefreshSessionResult.Error;
        }

        return true;
    }

    public Result<Error> DeleteRefreshSession(RefreshSession refreshSession)
    {
        var isRefreshSessionExist = HasRefreshSession(refreshSession);

        if (!isRefreshSessionExist)
            return Errors.General.RecordNotFound(nameof(RefreshSession));

        _refreshSessions.Remove(refreshSession);
        return true;
    }

    public Result<Error> DeleteRefreshSessions(IEnumerable<RefreshSession> refreshSessions)
    {
        foreach (var refreshSession in refreshSessions)
        {
            var deleteRefreshSessionsResult = DeleteRefreshSession(refreshSession);

            if (deleteRefreshSessionsResult.IsFailure)
                return deleteRefreshSessionsResult.Error;
        }

        return true;
    }

    #endregion
}