using Workshopper.Api.Auth.Contracts.Login;
using Workshopper.Application.Users.Commands.Login;

namespace Workshopper.Api.Auth.Login;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Login";
            s.ExampleRequest = new LoginRequest(
                "user@email.com",
                "P@ssword1!"
            );
        });
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var command = new LoginCommand(
            request.Email,
            request.Hash);

        var token = await command.ExecuteAsync(ct);

        await SendOkAsync(
            new LoginResponse(token),
            ct);
    }
}