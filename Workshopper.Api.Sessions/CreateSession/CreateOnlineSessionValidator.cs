using FluentValidation;
using Workshopper.Api.Sessions.Contracts.CreateSession;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateOnlineSessionValidator : Validator<CreateSessionRequest>
{
    public CreateOnlineSessionValidator()
    {
        RuleFor(x => x.Link)
            .NotEmpty()
            .MaximumLength(250);
    }
}