using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Features.Auth.Commands.Login;
using ToDoList.Application.Features.Auth.Commands.RefreshTokens;
using ToDoList.Application.Features.Auth.Commands.Register;
using ToDoList.Contracts.Requests;
using ToDoList.Presentation.Mappers;
using ToDoList.Presentation.Response;

namespace ToDoList.Presentation.Controllers;

public class AuthController : ApplicationController
{
    [HttpPost("sessions")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("tokens-renewal")]
    public async Task<IActionResult> RefreshTokens(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("users")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler handler,
        CancellationToken token)
    {
        var command = request.ToCommand();

        var result = await handler.HandleAsync(command, token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}