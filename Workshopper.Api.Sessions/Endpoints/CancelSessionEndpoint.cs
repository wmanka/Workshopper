using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands.CancelSession;

namespace Workshopper.Api.Sessions.Endpoints;

public class CancelSessionEndpoint : Endpoint<CancelSessionRequest>
{
    public override void Configure()
    {
        Post("/sessions/cancel");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create a session";
            s.ExampleRequest = new CancelSessionRequest(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CancelSessionRequest request, CancellationToken ct)
    {
        await new CancelSessionCommand
        {
            Id = request.Id
        }.ExecuteAsync(ct);

        await SendNoContentAsync(ct);
    }
}