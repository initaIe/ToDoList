﻿using Microsoft.AspNetCore.Identity;
using ToDoList.Application.Abstractions.Providers;

namespace ToDoList.Infrastructure.Providers;

public class PasswordHashProvider : IPasswordHashProvider
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string GenerateHash(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool IsPasswordValid(string passwordHash, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, passwordHash, password);

        return result switch
        {
            PasswordVerificationResult.Failed => false,
            PasswordVerificationResult.Success => true,
            PasswordVerificationResult.SuccessRehashNeeded => true,
            _ => false
        };
    }
}