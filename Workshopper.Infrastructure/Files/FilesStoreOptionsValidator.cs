using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.Files;

public class FilesStoreOptionsValidator : IValidateOptions<FilesStoreOptions>
{
    public ValidateOptionsResult Validate(string? name, FilesStoreOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BucketName))
        {
            return ValidateOptionsResult.Fail("AWS bucket name must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.AccessKeyId))
        {
            return ValidateOptionsResult.Fail("AWS access key id must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.SecretAccessKey))
        {
            return ValidateOptionsResult.Fail("AWS secret access key must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.Region))
        {
            return ValidateOptionsResult.Fail("AWS region must be provided");
        }

        var region = Amazon.RegionEndpoint.GetBySystemName(options.Region);
        if (region is null)
        {
            return ValidateOptionsResult.Fail("AWS region with specified name not found");
        }

        return ValidateOptionsResult.Success;
    }
}