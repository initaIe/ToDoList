using ToDoList.Domain.UsersManagement.AggregateRoot;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Application.Factories;

public static class UserFactory
{
    public static User ForceCreateNewUser(
        string username,
        string emailAddress,
        string passwordHash)
    {
        var userNameVo = Username.Create(username).Value;
        var emailAddressVo = EmailAddress.Create(emailAddress).Value;

        return User.CreateNew(userNameVo, emailAddressVo, passwordHash);
    }
}