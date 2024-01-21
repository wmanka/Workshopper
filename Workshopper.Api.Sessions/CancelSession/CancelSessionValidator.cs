using FluentValidation;
using Workshopper.Api.Sessions.Contracts.Sessions;

namespace Workshopper.Api.Sessions.CancelSession;

public class CancelSessionValidator : Validator<CancelSessionRequest>
{
    public CancelSessionValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}