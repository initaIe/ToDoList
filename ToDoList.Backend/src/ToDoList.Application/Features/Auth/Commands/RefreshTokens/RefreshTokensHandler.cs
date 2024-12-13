using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Application.Abstractions.Handlers;
using ToDoList.Application.Abstractions.Providers;
using ToDoList.Application.Validation;
using ToDoList.Contracts.Responses;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.AggregateRoot;
using ToDoList.Domain.UsersManagement.Entities;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Application.Features.Auth.Commands.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<RefreshTokensResponse, RefreshTokensCommand>
{
    private readonly IRepository<User, UserId> _userRepository;
    private readonly IToDoListReadDbContext _readDbContext;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RefreshTokensCommand> _validator;
    private readonly IRefreshSessionOptionsProvider _refreshSessionOptionsProvider;


    public RefreshTokensHandler(
        IRepository<User, UserId> userRepository,
        IValidator<RefreshTokensCommand> validator,
        ITokenProvider tokenProvider,
        IToDoListReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IRefreshSessionOptionsProvider refreshSessionOptionsProvider)
    {
        _userRepository = userRepository;
        _validator = validator;
        _tokenProvider = tokenProvider;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _refreshSessionOptionsProvider = refreshSessionOptionsProvider;
    }

    public async Task<Result<RefreshTokensResponse, ErrorList>> HandleAsync(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var accessTokenValidationResult =
                await _tokenProvider.ValidateAccessTokenWithoutLifeTimeAsync(command.AccessToken);
            if (accessTokenValidationResult.IsFailure)
                return accessTokenValidationResult.Error.ToErrorList();

            var accessTokenParseResult = _tokenProvider.ParseAccessToken(command.AccessToken);
            if (accessTokenParseResult.IsFailure)
                return accessTokenParseResult.Error.ToErrorList();

            var expiredUserRefreshSessionDataModel = await _readDbContext.Users
                .Include(u => u.RefreshSessions)
                .Where(u => u.Id == accessTokenParseResult.Value.AccountId)
                .SelectMany(u => u.RefreshSessions)
                .FirstOrDefaultAsync(rs =>
                        rs.Id == command.RefreshToken &&
                        rs.Jti == accessTokenParseResult.Value.Jti,
                    cancellationToken);

            if (expiredUserRefreshSessionDataModel == null)
                return Errors.Auth.TokenIsInvalid().ToErrorList();

            if (expiredUserRefreshSessionDataModel.ExpiresAt < DateTimeOffset.UtcNow)
                return Errors.Auth.ExpiredToken("RefreshToken").ToErrorList();

            var userId = UserId.Create(accessTokenParseResult.Value.AccountId).Value;
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            var refreshSessionId = RefreshSessionId.Create(expiredUserRefreshSessionDataModel.Id).Value;
            var currentUserRefreshSession = user.Value.GetRefreshSessionById(refreshSessionId);

            var newJti = Jti.CreateRandom();
            var expiresInDays = _refreshSessionOptionsProvider.GetExpireInDays();
            var expiresAt = RefreshSessionExpiresAt.Create(expiresInDays).Value;

            var newAccessToken = _tokenProvider.GenerateAccessToken(user.Value.Id.Value, newJti.Value);
            var newRefreshSession = RefreshSession.CreateNew(newJti, expiresAt);

            var response = new RefreshTokensResponse(newAccessToken, newRefreshSession.Id.Value);

            user.Value.DeleteRefreshSession(currentUserRefreshSession.Value);
            user.Value.AddRefreshSession(newRefreshSession);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Errors.Auth.RefreshTokensFailure().ToErrorList();
        }
    }
}