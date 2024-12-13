using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects;
using ToDoList.Domain.Shared.ValueObjectsManagement.ValueObjects.Ids;
using ToDoList.Domain.ToDoItemManagement.AggregateRoot;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjects;
using ToDoList.Domain.ToDoItemManagement.ValueObjectManagement.ValueObjectsConstraints;

namespace ToDoList.Infrastructure.Configurations.Write;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        // TABLE NAMING
        builder.ToTable("to_do_items");

        // ID
        builder.HasKey(toDoItem => toDoItem.Id);
        builder.Property(toDoItem => toDoItem.Id)
            .HasConversion(
                id => id.Value,
                value => ToDoItemId.Create(value).Value)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(toDoItem => toDoItem.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => CreatedAt.Create(value).Value)
            .HasColumnName("created_at")
            .IsRequired();

        // TITLE
        builder.Property(toDoItem => toDoItem.Title)
            .HasConversion(
                title => title.Value,
                value => Title.Create(value).Value)
            .HasMaxLength(TitleConstraints.MaxLength)
            .HasColumnName("title")
            .IsRequired();
        // TITLE_INDEX
        builder.HasIndex(toDoItem => toDoItem.Title)
            .HasDatabaseName("idx_unique_title")
            .IsUnique();

        // IS_COMPLETED
        builder.Property(toDoItem => toDoItem.IsCompleted)
            .HasColumnName("is_completed")
            .IsRequired();
    }
}