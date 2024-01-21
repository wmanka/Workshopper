using FluentValidation;
using Workshopper.Api.Contracts.Subscriptions;

namespace Workshopper.Api.Subscriptions;

public class CreateSubscriptionValidator : Validator<CreateSubscriptionRequest>
{
    public CreateSubscriptionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.SubscriptionType)
            .NotNull();
    }
}