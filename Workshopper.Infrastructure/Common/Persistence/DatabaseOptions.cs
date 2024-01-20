namespace Workshopper.Infrastructure.Common.Persistence;

public class DatabaseOptions
{
    /// <summary>
    /// Section name in configuration
    /// </summary>
    public const string SectionName = "Database";

    /// <summary>
    /// Connection string to database
    /// </summary>
    public string ConnectionString { get; set; } = null!;
}