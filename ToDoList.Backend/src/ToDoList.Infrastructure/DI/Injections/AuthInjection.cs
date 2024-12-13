using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Factories;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.DI.Injections;

public static class AuthInjection
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtAccessTokenOptions = configuration
                    .GetRequiredSection(JwtBearerAuthOptions.SectionName)
                    .Get<JwtBearerAuthOptions>();

                options.TokenValidationParameters = TokenValidationParametersFactory.Create(jwtAccessTokenOptions!);
            });

        services.AddAuthorization();

        return services;
    }
}