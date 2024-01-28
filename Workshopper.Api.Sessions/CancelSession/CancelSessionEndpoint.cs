using Workshopper.Api.Sessions.Contracts.CancelSession;
using Workshopper.Application.Common.Models;
using Workshopper.Application.Sessions.Commands.CancelSession;

namespace Workshopper.Api.Sessions.CancelSession;

public class CancelSessionEndpoint : Endpoint<CancelSessionRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/sessions/cancel");
        Roles(DomainRoles.Host);
        Summary(s =>
        {
            s.Summary = "Cancel a session";
            s.ExampleRequest = new CancelSessionRequest(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CancelSessionRequest request, CancellationToken ct)
    {
        var command = new CancelSessionCommand(request.Id);
        await command.ExecuteAsync(ct);

        await SendOkAsync(ct);
    }
}