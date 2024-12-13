using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Domain.ToDoItemManagement.AggregateRoot;
using ToDoList.Domain.UsersManagement.AggregateRoot;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.DbContexts;

public class ToDoListWriteDbContext : DbContext
{
    private readonly SqliteOptions _sqliteOptions;

    public ToDoListWriteDbContext(IOptions<SqliteOptions> sqliteOptions)
    {
        _sqliteOptions = sqliteOptions.Value;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(_sqliteOptions.ConnectionString)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ToDoListWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}