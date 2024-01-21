using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands.CancelSession;

namespace Workshopper.Api.Sessions.Endpoints;

public class CancelSessionEndpoint : Endpoint<CancelSessionRequest,
    Results<NoContent, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/sessions/cancel");
        AllowAnonymous();
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create a session";
            s.ExampleRequest = new CancelSessionRequest(Guid.NewGuid());
        });
    }

    public override async Task<Results<NoContent, NotFound, ProblemDetails>> ExecuteAsync(
        CancelSessionRequest req, CancellationToken ct)
    {
        await new CancelSessionCommand
        {
            Id = req.Id
        }.ExecuteAsync(ct);

        return TypedResults.NoContent();
    }
}