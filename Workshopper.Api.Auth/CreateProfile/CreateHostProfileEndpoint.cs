using Workshopper.Api.Auth.Contracts.CreateProfile;
using Workshopper.Application.Users.Commands.CreateProfile;

namespace Workshopper.Api.Auth.CreateProfile;

public class CreateHostProfileEndpoint : Endpoint<CreateHostProfileRequest, Guid>
{
    public override void Configure()
    {
        Post("/profiles/host");
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create Host Profile";
            s.ExampleRequest = new CreateHostProfileRequest(
                "John",
                "Doe",
                "Software Engineer",
                "Microsoft",
                "I am a software engineer at Microsoft"
            );
        });
    }

    public override async Task HandleAsync(CreateHostProfileRequest request, CancellationToken ct)
    {
        var command = new CreateHostProfileCommand(
            request.FirstName,
            request.LastName,
            request.Title,
            request.Company,
            request.Bio);

        var profileId = await command.ExecuteAsync(ct);

        await SendOkAsync(profileId, ct);
    }
}