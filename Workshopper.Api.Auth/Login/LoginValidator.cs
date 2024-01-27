using FluentValidation;

namespace Workshopper.Api.Auth.Login;

public class LoginValidator : Validator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(250);
    }
}