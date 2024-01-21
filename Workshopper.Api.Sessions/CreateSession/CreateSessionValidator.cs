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

        RuleFor(x => x.Description)
            .MaximumLength(250);

        RuleFor(x => x.StartDateTime)
            .NotNull()
            .GreaterThan(DateTimeOffset.Now);

        RuleFor(x => x.EndDateTime)
            .NotNull()
            .GreaterThan(x => x.StartDateTime);

        RuleFor(x => x.Places)
            .NotNull()
            .GreaterThanOrEqualTo(1);

        When((request, context) => request.DeliveryType == DeliveryType.Online,
            () =>
            {
                Include(new CreateOnlineSessionValidator());
            });

        When((request, context) => request.DeliveryType == DeliveryType.Stationary,
            () =>
            {
                Include(new CreateStationarySessionValidator());
            });
    }
}