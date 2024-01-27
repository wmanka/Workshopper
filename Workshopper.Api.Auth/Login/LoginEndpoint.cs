using FastEndpoints.Security;

namespace Workshopper.Api.Auth.Login;

public class LoginEndpoint : Endpoint<LoginRequest>
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
                "p@ssword"
            );
        });
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        await SendOkAsync(ct);
        // if (await authService.CredentialsAreValid(req.Email, req.Password, ct))
        // {
        //     var token = JWTBearer.CreateToken(
        //         signingKey: "TokenSigningKey",
        //         expireAt: DateTime.UtcNow.AddDays(1),
        //         priviledges: u =>
        //         {
        //             // u.Roles.Add("Manager");
        //             // u.Permissions.AddRange(new[] { "ManageUsers", "ManageInventory" });
        //             u.Claims.Add(new("Email", req.Email));
        //             u["UserID"] = "001"; //indexer based claim setting
        //         });
        //
        //     await SendAsync(new
        //         {
        //             Email = req.Email,
        //             Token = token
        //         },
        //         cancellation: ct);
        // }
        // else
        // {
        //     ThrowError("The supplied credentials are invalid!");
        // }
    }
}