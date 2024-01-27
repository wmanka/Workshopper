using Workshopper.Application.Users.Commands.Register;

namespace Workshopper.Api.Auth.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Register new user";
            s.ExampleRequest = new RegisterRequest(
                "user@email.com",
                "P@ssword1!"
            );
        });
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        var command = new RegisterCommand(
            request.Email,
            request.Password);

        await command.ExecuteAsync(ct);

        await SendOkAsync(ct);
    }
}