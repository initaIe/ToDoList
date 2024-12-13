using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Application.DataModels;

namespace ToDoList.Infrastructure.Configurations.Read;

public class RefreshSessionDataModelConfiguration : IEntityTypeConfiguration<RefreshSessionDataModel>
{
    public void Configure(EntityTypeBuilder<RefreshSessionDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("refresh_sessions");

        // ID
        builder.Property(refreshSession => refreshSession.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(refreshSession => refreshSession.CreatedAt)
            .HasColumnName("created_at");

        // JTI
        builder.Property(refreshSession => refreshSession.Jti)
            .HasColumnName("jti");

        // EXPIRES_AT
        builder.Property(refreshSession => refreshSession.ExpiresAt)
            .HasColumnName("expires_at");

        // USER_ID
        builder.Property(refreshSession => refreshSession.UserId)
            .HasColumnName("user_id");
    }
}