namespace Workshopper.Api.Auth.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Register new user";
            s.ExampleRequest = new RegisterRequest(
                "user@email.com",
                "p@ssword"
            );
        });
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}