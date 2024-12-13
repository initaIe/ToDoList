using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.UsersManagement.Entities;
using ToDoList.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace ToDoList.Infrastructure.Configurations.Write;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.HasKey(refreshSession => refreshSession.Id);
        builder.Property(refreshSession => refreshSession.Id)
            .HasConversion(
                id => id.Value,
                value => RefreshSessionId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(refreshSession => refreshSession.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // JTI
        builder.Property(refreshSession => refreshSession.Jti)
            .HasConversion(
                jti => jti.Value,
                value => Jti.Create(value).Value)
            .HasColumnName("jti")
            .IsRequired();

        // EXPIRES_AT
        builder.Property(refreshSession => refreshSession.ExpiresAt)
            .HasConversion(
                expiresAt => expiresAt.Value,
                value => RefreshSessionExpiresAt.Create(value).Value)
            .HasColumnName("expires_at")
            .IsRequired();
    }
}