using FluentValidation;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateOnlineSessionCommandValidator : AbstractValidator<CreateOnlineSessionCommand>
{
    public CreateOnlineSessionCommandValidator(
        ISessionsRepository sessionsRepository,
        ICurrentUserProvider currentUserProvider)
    {
        RuleFor(x => x.SessionType)
            .NotNull();

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                var userProfile = currentUserProvider.GetCurrentUser();
                var userProfileId = userProfile?.ProfileId;
                if (userProfile is null || userProfileId is null || !userProfile.IsHost)
                {
                    context.AddFailure(SessionErrors.OnlyHostCanCreateSession);

                    return;
                }

                if (await sessionsRepository.AnyAsync(
                        new SessionDuringTimeSpecification(command.StartDateTime, command.EndDateTime, userProfileId.Value)))
                {
                    context.AddFailure(SessionErrors.SessionTimeOverlaps);
                }
            });

        RuleFor(x => x.Link)
            .NotEmpty()
            .MaximumLength(250);
    }
}