using ToDoList.Application.Models;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;

namespace ToDoList.Application.Abstractions.Providers;

public interface ITokenProvider
{
    string GenerateAccessToken(Guid accountId, Guid jti);
    Result<AccessTokenParseResult, Error> ParseAccessToken(string token);
    Task<Result<Error>> ValidateAccessTokenWithoutLifeTimeAsync(string token);
}