using Workshopper.Api.Auth.Contracts;
using Workshopper.Api.Auth.Contracts.ChangeProfile;
using Workshopper.Application.Users.Commands.ChangeProfile;
using DomainProfileType = Workshopper.Domain.Users.UserProfiles.ProfileType;

namespace Workshopper.Api.Auth.ChangeProfile;

public class ChangeProfileEndpoint : Endpoint<ChangeProfileRequest, AuthenticationResponse>
{
    public override void Configure()
    {
        Post("/profiles/change");
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create Host Profile";
            s.ExampleRequest = new ChangeProfileRequest(
                ProfileType.Host
            );

            s.ExampleRequest = new ChangeProfileRequest(
                ProfileType.Attendee
            );
        });
    }

    public override async Task HandleAsync(ChangeProfileRequest request, CancellationToken ct)
    {
        var command = new ChangeProfileCommand(
            DomainProfileType.FromName(request.ProfileType.ToString()));

        var token = await command.ExecuteAsync(ct);

        await SendOkAsync(
            new AuthenticationResponse(token),
            ct);
    }
}