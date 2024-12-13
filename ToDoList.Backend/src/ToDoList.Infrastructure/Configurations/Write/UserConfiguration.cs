using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.AggregateRoot;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace ToDoList.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(user => user.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // USER_NAME
        builder.Property(user => user.Username)
            .HasConversion(
                userName => userName!.Value,
                value => Username.Create(value).Value)
            .HasMaxLength(UsernameConstraints.MaxLength)
            .HasColumnName("username")
            .IsRequired();

        // EMAIL_ADDRESS
        builder.Property(user => user.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress!.Value,
                value => EmailAddress.Create(value).Value)
            .HasMaxLength(EmailAddressConstraints.MaxLength)
            .HasColumnName("email_address")
            .IsRequired();

        // PHONE_NUMBER
        builder.Property(user => user.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value).Value)
            .HasMaxLength(PhoneNumberConstraints.MaxLength)
            .HasColumnName("phone_number")
            .IsRequired(false);

        // PASSWORD_HASH
        builder.Property(user => user.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        // REFRESH_SESSIONS
        builder.HasMany(u => u.RefreshSessions)
            .WithOne()
            .HasForeignKey("user_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}