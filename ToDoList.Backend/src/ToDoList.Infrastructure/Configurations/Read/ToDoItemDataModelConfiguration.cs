using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Application.DataModels;

namespace ToDoList.Infrastructure.Configurations.Read;

public class ToDoItemDataModelConfiguration : IEntityTypeConfiguration<ToDoItemDataModel>
{
    public void Configure(EntityTypeBuilder<ToDoItemDataModel> builder)
    {
        // TABLE NAMING
        builder.ToTable("to_do_items");

        // ID
        builder.Property(toDoItem => toDoItem.Id)
            .HasColumnName("id");

        // CREATED_AT
        builder.Property(toDoItem => toDoItem.CreatedAt)
            .HasColumnName("created_at");

        // TITLE
        builder.Property(toDoItem => toDoItem.Title)
            .HasColumnName("title");

        // IS_COMPLETED
        builder.Property(toDoItem => toDoItem.IsCompleted)
            .HasColumnName("is_completed");
    }
}