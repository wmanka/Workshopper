using FastEndpoints;
using FluentValidation;
using Workshopper.Contracts.Subscriptions;

namespace Workshopper.Api.Validators;

public class CreateSubscriptionValidator : Validator<CreateSubscriptionRequest>
{
    public CreateSubscriptionValidator()
    {
        RuleFor(x => x.AdminId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("Admin id is required");

        RuleFor(x => x.SubscriptionType)
            .NotNull()
            .WithMessage("Subscription type is required");
    }
}