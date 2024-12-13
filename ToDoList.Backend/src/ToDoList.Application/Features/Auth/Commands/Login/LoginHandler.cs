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

namespace ToDoList.Application.Features.Auth.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly IValidator<LoginCommand> _commandValidator;
    private readonly IRepository<User, UserId> _accountRepository;
    private readonly IToDoListReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshSessionOptionsProvider _refreshSessionOptionsProvider;

    public LoginHandler(
        IValidator<LoginCommand> commandValidator,
        IRepository<User, UserId> accountRepository,
        IToDoListReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        ITokenProvider tokenProvider,
        IRefreshSessionOptionsProvider refreshSessionOptionsProvider)
    {
        _commandValidator = commandValidator;
        _accountRepository = accountRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _tokenProvider = tokenProvider;
        _refreshSessionOptionsProvider = refreshSessionOptionsProvider;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var accountByEmailAddress = await _readDbContext.Users.FirstOrDefaultAsync(
                a => a.EmailAddress == command.EmailAddress,
                cancellationToken);

            if (accountByEmailAddress == null)
                return Errors.Auth.CredentialsAreInvalid().ToErrorList();

            var isPasswordValid = _passwordHashProvider.IsPasswordValid(
                accountByEmailAddress.PasswordHash,
                command.Password);

            if (!isPasswordValid)
                return Errors.Auth.CredentialsAreInvalid().ToErrorList();

            var jti = Jti.CreateRandom();

            var expiresInDays = _refreshSessionOptionsProvider.GetExpireInDays();
            var expiresAt = RefreshSessionExpiresAt.Create(expiresInDays).Value;

            var refreshSession = RefreshSession.CreateNew(jti, expiresAt);
            var accessToken = _tokenProvider.GenerateAccessToken(accountByEmailAddress.Id, jti.Value);
            var response = new LoginResponse(accessToken, refreshSession.Id.Value);

            var accountId = UserId.Create(accountByEmailAddress.Id).Value;
            var getAccountResult = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
            getAccountResult.Value.AddRefreshSession(refreshSession);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Errors.Auth.LoginFailure().ToErrorList();
        }
    }
}