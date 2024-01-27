using Workshopper.Api.Sessions.Contracts.CancelSession;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Api.Sessions.CancelSession;

public class CancelSessionEndpoint : Endpoint<CancelSessionRequest, EmptyResponse, CancelSessionMapper>
{
    public override void Configure()
    {
        Post("/sessions/cancel");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Cancel a session";
            s.ExampleRequest = new CancelSessionRequest(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CancelSessionRequest request, CancellationToken ct)
    {
        var command = Map.ToEntity(request);
        await command.ExecuteAsync(ct);

        await SendOkAsync(ct);
    }
}