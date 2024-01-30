using Workshopper.Api.Sessions.Contracts.RegisterForSession;
using Workshopper.Application.Common.Models;
using Workshopper.Application.Sessions.Commands.RegisterForSession;

namespace Workshopper.Api.Sessions.RegisterForSession;

public class RegisterForSessionEndpoint : Endpoint<RegisterForSessionRequest>
{
    public override void Configure()
    {
        Post("/sessions/register");
        Roles(DomainRoles.Attendee);
        Summary(s =>
        {
            s.Summary = "Register for a session";
            s.ExampleRequest = new RegisterForSessionRequest(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(RegisterForSessionRequest request, CancellationToken ct)
    {
        var command = new RegisterForSessionCommand(request.Id);
        await command.ExecuteAsync(ct);

        await SendOkAsync(ct);
    }
}