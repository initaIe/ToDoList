using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Application.Abstractions.Database;
using ToDoList.Application.DataModels;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.DbContexts;

public class ToDoListReadDbContext : DbContext, IToDoListReadDbContext
{
    private readonly SqliteOptions _sqliteOptions;

    public ToDoListReadDbContext(IOptions<SqliteOptions> sqliteOptions)
    {
        _sqliteOptions = sqliteOptions.Value;
    }

    public IQueryable<UserDataModel> Users => Set<UserDataModel>();
    public IQueryable<ToDoItemDataModel> ToDoItems => Set<ToDoItemDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(_sqliteOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ToDoListReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}