using FluentValidation;
using Workshopper.Api.Auth.Contracts.ChangeProfile;

namespace Workshopper.Api.Auth.ChangeProfile;

public class ChangeProfileEndpointValidator : Validator<ChangeProfileRequest>
{
    public ChangeProfileEndpointValidator()
    {
        RuleFor(x => x.ProfileType)
            .NotNull()
            .IsInEnum();
    }
}