using FluentValidation;

namespace Workshopper.Api.Auth.Register;

public class RegisterValidator : Validator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(250);
    }
}