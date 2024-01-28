using FluentValidation;
using Workshopper.Application.Common.Abstractions;
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
                        command.EndDateTime)))
                {
                    context.AddFailure(SessionErrors.SessionTimeOverlaps);
                }
            });
    }
}