using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.Common.Persistence;

public class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>
{
    public ValidateOptionsResult Validate(string? name, DatabaseOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail("Database connection string must be provided");
        }

        return ValidateOptionsResult.Success;
    }
}