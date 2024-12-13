using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Application.Abstractions.Providers;
using ToDoList.Application.Models;
using ToDoList.Domain.Shared.ErrorManagement;
using ToDoList.Domain.Shared.Others;
using ToDoList.Infrastructure.Factories;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.Providers;

public static class CustomClaims
{
    public const string Sub = nameof(Sub);
    public const string Jti = nameof(Jti);
}

public class TokenProvider : ITokenProvider
{
    private readonly IOptionsMonitor<JwtBearerAuthOptions> _jwtBearerOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public TokenProvider(IOptionsMonitor<JwtBearerAuthOptions> options)
    {
        _jwtBearerOptions = options;
    }

    public string GenerateAccessToken(Guid accountId, Guid jti)
    {
        var claims = new[]
        {
            new Claim(CustomClaims.Sub, accountId.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerOptions.CurrentValue.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var nbfDateTime = DateTime.UtcNow;
        var expDateTime = DateTime.UtcNow.AddMinutes(_jwtBearerOptions.CurrentValue.ExpiresInMinutes);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtBearerOptions.CurrentValue.Issuer,
            audience: _jwtBearerOptions.CurrentValue.Audience,
            claims: claims,
            notBefore: nbfDateTime,
            expires: expDateTime,
            signingCredentials: signingCredentials);

        return _jwtSecurityTokenHandler.WriteToken(jwtToken);
    }

    public Result<AccessTokenParseResult, Error> ParseAccessToken(string token)
    {
        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

        if (jwtSecurityToken == null)
            return Errors.Auth.TokenIsInvalid();

        var subClaim = jwtSecurityToken.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.Sub)?.Value;

        if (!Guid.TryParse(subClaim, out var accountId))
            return Errors.Auth.TokenIsInvalid();

        var jtiClaim = jwtSecurityToken.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.Jti)?.Value;

        if (!Guid.TryParse(jtiClaim, out var jti))
            return Errors.Auth.TokenIsInvalid();

        return new AccessTokenParseResult(accountId, jti);
    }

    public async Task<Result<Error>> ValidateAccessTokenWithoutLifeTimeAsync(string token)
    {
        var tokenValidationParameters = TokenValidationParametersFactory
            .CreateWithoutValidationLifeTime(_jwtBearerOptions.CurrentValue);

        var validationResult = await _jwtSecurityTokenHandler.ValidateTokenAsync(
            token,
            tokenValidationParameters);

        if (!validationResult.IsValid)
            return Errors.Auth.TokenIsInvalid();

        return true;
    }
}