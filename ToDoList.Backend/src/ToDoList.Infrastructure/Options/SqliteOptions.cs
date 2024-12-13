namespace ToDoList.Infrastructure.Options;

public class SqliteOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(SqliteOptions);

    public string ConnectionString { get; init; } = null!;
}