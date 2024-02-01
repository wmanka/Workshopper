using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.Authentication;

public class JwtOptionsValidator : IValidateOptions<JwtOptions>
{
    public ValidateOptionsResult Validate(string? name, JwtOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            return ValidateOptionsResult.Fail("Audience must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            return ValidateOptionsResult.Fail("Issuer must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.SigningKey))
        {
            return ValidateOptionsResult.Fail("Signing key must be provided");
        }

        if (options.TokenExpiration <= 0)
        {
            return ValidateOptionsResult.Fail("Token expiration must be greater than 0");
        }

        return ValidateOptionsResult.Success;
    }
}