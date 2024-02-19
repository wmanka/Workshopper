using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.Files;

public class FilesStoreOptionsValidator : IValidateOptions<FilesStoreOptions>
{
    public ValidateOptionsResult Validate(string? name, FilesStoreOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BucketName))
        {
            return ValidateOptionsResult.Fail("s3 bucket name must be provided");
        }

        return ValidateOptionsResult.Success;
    }
}