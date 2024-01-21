using FluentValidation;
using Workshopper.Api.Sessions.Contracts.Sessions;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateSessionValidator : Validator<CreateSessionRequest>
{
    public CreateSessionValidator()
    {
        RuleFor(x => x.DeliveryType)
            .NotNull()
            .Custom((deliveryType, context) =>
            {
                if (!DomainDeliveryType.TryFromName(deliveryType.ToString(), out _))
                {
                    context.AddFailure("Invalid delivery type");
                }
            });

        RuleFor(x => x.SessionType)
            .NotNull()
            .Custom((sessionType, context) =>
            {
                if (!DomainSessionType.TryFromName(sessionType.ToString(), out _))
                {
                    context.AddFailure("Invalid session type");
                }
            });

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(250);
    }
}