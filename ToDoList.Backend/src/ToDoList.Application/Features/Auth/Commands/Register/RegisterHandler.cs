using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Application.Abstractions.Handlers;
using ToDoList.Application.Abstractions.Providers;
using ToDoList.Application.Factories;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.AggregateRoot;

namespace ToDoList.Application.Features.Auth.Commands.Register;

public class RegisterHandler : ICommandHandler<Guid, RegisterCommand>
{
    private readonly IValidator<RegisterCommand> _commandValidator;
    private readonly IRepository<User, UserId> _userRepository;
    private readonly IToDoListReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public RegisterHandler(
        IValidator<RegisterCommand> commandValidator,
        IRepository<User, UserId> userRepository,
        IToDoListReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider)
    {
        _commandValidator = commandValidator;
        _userRepository = userRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        RegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var isUsernameOrEmailAddressAlreadyTaken = await _readDbContext.Users.AnyAsync(
                a => a.Username.ToLower() == command.Username.ToLower()
                     || a.EmailAddress.ToLower() == command.EmailAddress.ToLower(),
                cancellationToken);

            if (isUsernameOrEmailAddressAlreadyTaken)
                return Errors.General.RecordAlreadyExist(nameof(User)).ToErrorList();

            var passwordHash = _passwordHashProvider.GenerateHash(command.Password);

            var user = UserFactory.ForceCreateNewUser(command.Username, command.EmailAddress, passwordHash);

            await _userRepository.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return user.Id.Value;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Errors.Auth.RegistrationFailure().ToErrorList();
        }
    }
}