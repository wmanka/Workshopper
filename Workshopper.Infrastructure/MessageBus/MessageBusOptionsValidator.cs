using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.MessageBus;

public class MessageBusOptionsValidator : IValidateOptions<MessageBusOptions>
{
    public ValidateOptionsResult Validate(string? name, MessageBusOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.User))
        {
            return ValidateOptionsResult.Fail("User must be provided");
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            return ValidateOptionsResult.Fail("Password must be provided");
        }

        return ValidateOptionsResult.Success;
    }
}