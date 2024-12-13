using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Application.Abstractions.Services;
using ToDoList.Application.DataModels;
using ToDoList.Application.Factories;
using ToDoList.Application.Features.ToDoItems.Commands;
using ToDoList.Application.Features.ToDoItems.Queries;
using ToDoList.Application.Validation;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.ToDoItemManagement.AggregateRoot;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;

namespace ToDoList.Application.Features.ToDoItems;

public class ToDoItemService : IToDoItemService
{
    private readonly IValidator<CreateToDoItemCommand> _createCommandValidator;
    private readonly IValidator<UpdateToDoItemCommand> _updateCommandValidator;
    private readonly IValidator<DeleteToDoItemCommand> _deleteCommandValidator;
    private readonly IValidator<GetToDoItemByIdQuery> _getByIdQueryValidator;
    private readonly IToDoListReadDbContext _readDbContext;
    private readonly IRepository<ToDoItem, ToDoItemId> _toDoItemsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToDoItemService(
        IValidator<CreateToDoItemCommand> createCommandValidator,
        IToDoListReadDbContext readDbContext,
        IRepository<ToDoItem, ToDoItemId> toDoItemsRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateToDoItemCommand> updateCommandValidator,
        IValidator<DeleteToDoItemCommand> deleteCommandValidator,
        IValidator<GetToDoItemByIdQuery> getByIdQueryValidator)
    {
        _createCommandValidator = createCommandValidator;
        _readDbContext = readDbContext;
        _toDoItemsRepository = toDoItemsRepository;
        _unitOfWork = unitOfWork;
        _updateCommandValidator = updateCommandValidator;
        _deleteCommandValidator = deleteCommandValidator;
        _getByIdQueryValidator = getByIdQueryValidator;
    }

    public async Task<Result<Guid, ErrorList>> CreateAsync(
        CreateToDoItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _createCommandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var isTitleAlreadyTaken = await _readDbContext.ToDoItems.AnyAsync(
                toDoItem => toDoItem.Title.ToLower() == command.Title.ToLower(),
                cancellationToken);

            if (isTitleAlreadyTaken)
                return Errors.General.RecordAlreadyExist(
                        nameof(ToDoItem),
                        nameof(Title))
                    .ToErrorList();

            var toDoItem = ToDoItemFactory.ForceCreateNewToDoItem(command.Title);

            await _toDoItemsRepository.AddAsync(toDoItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return toDoItem.Id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure(
                    "create.was.failure",
                    "Create ToDoItem was failure.")
                .ToErrorList();
        }
    }

    public async Task<Result<Guid, ErrorList>> UpdateAsync(
        UpdateToDoItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _updateCommandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var isTitleAlreadyTaken = await _readDbContext.ToDoItems.AnyAsync(
                toDoItem => toDoItem.Title.ToLower() == command.Title.ToLower(),
                cancellationToken);

            if (isTitleAlreadyTaken)
                return Errors.General.RecordAlreadyExist(
                        nameof(ToDoItem),
                        nameof(Title))
                    .ToErrorList();

            var id = ToDoItemId.Create(command.Id).Value;

            var toDoItem = await _toDoItemsRepository.GetByIdAsync(id, cancellationToken);

            if (toDoItem.IsFailure)
                return Errors.General.RecordNotFound(
                        nameof(ToDoItem),
                        nameof(ToDoItemId))
                    .ToErrorList();

            var title = Title.Create(command.Title).Value;

            toDoItem.Value.Update(title, command.IsCompleted);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return toDoItem.Value.Id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure(
                    "update.was.failure",
                    "Update ToDoItem was failure.")
                .ToErrorList();
        }
    }

    public async Task<Result<Guid, ErrorList>> DeleteAsync(
        DeleteToDoItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _deleteCommandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var id = ToDoItemId.Create(command.Id).Value;

            var toDoItem = await _toDoItemsRepository.GetByIdAsync(id, cancellationToken);

            if (toDoItem.IsFailure)
                return Errors.General.RecordNotFound(
                        nameof(ToDoItem),
                        nameof(ToDoItemId))
                    .ToErrorList();

            _toDoItemsRepository.Delete(toDoItem.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return toDoItem.Value.Id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure(
                    "delete.was.failure",
                    "Delete ToDoItem was failure.")
                .ToErrorList();
        }
    }

    public async Task<Result<ToDoItemDataModel, ErrorList>> GetByIdAsync(
        GetToDoItemByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _getByIdQueryValidator.ValidateAsync(query, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var toDoItem = await _readDbContext.ToDoItems.FirstOrDefaultAsync(
            toDoItem => toDoItem.Id == query.Id,
            cancellationToken);

        if (toDoItem == null)
            return Errors.General.RecordNotFound(
                    nameof(ToDoItem),
                    nameof(ToDoItemId))
                .ToErrorList();

        return toDoItem;
    }

    public async Task<IReadOnlyList<ToDoItemDataModel>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var toDoItem = await _readDbContext.ToDoItems.ToListAsync(cancellationToken);
        return toDoItem;
    }
}