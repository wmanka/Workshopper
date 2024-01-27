using FluentValidation;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateOnlineSessionCommandValidator : Validator<CreateOnlineSessionCommand>
{
    public CreateOnlineSessionCommandValidator()
    {
        RuleFor(x => x)
            .CustomAsync(async (command, context, ct) =>
            {
                var repository = Resolve<ISessionsRepository>();
                if (await repository.AnyAsync(new SessionDuringTimeSpecification(
                        command.StartDateTime,
                        command.EndDateTime,
                        command.HostProfileId)))
                {
                    context.AddFailure(SessionErrors.SessionTimeOverlaps);
                }
            });
    }
}