using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.FeatureFlags;

public class FeatureFlagsOptionsValidator : IValidateOptions<FeatureFlagsSettings>
{
    public ValidateOptionsResult Validate(string? name, FeatureFlagsSettings options)
    {
        if (string.IsNullOrWhiteSpace(options.Key))
        {
            return ValidateOptionsResult.Fail("Feature flags provider key must be provided");
        }

        return ValidateOptionsResult.Success;
    }
}