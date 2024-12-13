using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Abstractions.Services;
using ToDoList.Application.Features.ToDoItems.Commands;
using ToDoList.Application.Features.ToDoItems.Queries;
using ToDoList.Contracts.Requests;
using ToDoList.Presentation.Mappers;
using ToDoList.Presentation.Response;

namespace ToDoList.Presentation.Controllers;

[Authorize]
public class ToDoItemsController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateToDoItemRequest request,
        [FromServices] IToDoItemService service,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await service.CreateAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] IToDoItemService service,
        CancellationToken token)
    {
        var result = await service.GetAllAsync(token);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] IToDoItemService service,
        CancellationToken token)
    {
        var query = new GetToDoItemByIdQuery(id);

        var result = await service.GetByIdAsync(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateToDoItemRequest request,
        [FromServices] IToDoItemService service,
        CancellationToken token)
    {
        var command = request.ToCommand(id);

        var result = await service.UpdateAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] IToDoItemService service,
        CancellationToken token)
    {
        var command = new DeleteToDoItemCommand(id);

        var result = await service.DeleteAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}