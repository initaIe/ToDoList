using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Application.DataModels;

namespace ToDoList.Infrastructure.Configurations.Read;

public class UserDataModelConfiguration : IEntityTypeConfiguration<UserDataModel>
{
    public void Configure(EntityTypeBuilder<UserDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("users");

        // ID
        builder.Property(user => user.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(user => user.CreatedAt)
            .HasColumnName("created_at");

        // USER_NAME
        builder.Property(user => user.Username)
            .HasColumnName("username");

        // EMAIL_ADDRESS
        builder.Property(user => user.EmailAddress)
            .HasColumnName("email_address");

        // PHONE_NUMBER
        builder.Property(user => user.PhoneNumber)
            .HasColumnName("phone_number");

        // PASSWORD_HASH
        builder.Property(user => user.PasswordHash)
            .HasColumnName("password_hash");

        // REFRESH_SESSIONS
        builder.HasMany(u => u.RefreshSessions)
            .WithOne()
            .HasForeignKey(rs => rs.UserId);
    }
}