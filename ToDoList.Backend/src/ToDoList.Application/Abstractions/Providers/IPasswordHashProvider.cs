namespace ToDoList.Application.Abstractions.Providers;

public interface IPasswordHashProvider
{
    string GenerateHash(string password);

    bool IsPasswordValid(string password, string passwordHash);
}