using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands.CreateSession;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.Endpoints;

public class CreateSessionEndpoint : Endpoint<CreateSessionRequest,
    Results<Ok<CreateSessionResponse>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create a session";
            s.ExampleRequest = new CreateSessionRequest(
                DeliveryType.Online,
                SessionType.Lecture,
                "Git Basics",
                null,
                DateTimeOffset.Now,
                DateTimeOffset.Now,
                50,
                "https://zoom.us/j/1234567890?pwd=QWERTYUIOPASDFGHJKLZXCVBNM",
                null);

            s.ResponseExamples[200] = new CreateSessionResponse(Guid.NewGuid());
        });
    }

    public override async Task<Results<Ok<CreateSessionResponse>, NotFound, ProblemDetails>> ExecuteAsync(
        CreateSessionRequest req, CancellationToken ct)
    {
        if (!DomainDeliveryType.TryFromName(req.DeliveryType.ToString(), out var deliveryType))
        {
            ThrowError(x => x.DeliveryType, "Invalid delivery type");
        }

        if (!DomainSessionType.TryFromName(req.SessionType.ToString(), out var sessionType))
        {
            ThrowError(x => x.SessionType, "Invalid session type");
        }

        var sessionId = Guid.Empty;
        if (deliveryType == DomainDeliveryType.Online) // todo:
        {
            sessionId = await new CreateOnlineSessionCommand
            {
                SessionType = sessionType,
                Title = req.Title,
                Description = req.Description,
                StartDateTime = req.StartDateTime,
                EndDateTime = req.EndDateTime,
                Places = req.Places,
                Link = req.Link!
            }.ExecuteAsync(ct);
        }
        else if (deliveryType == DomainDeliveryType.Stationary)
        {
        }
        else
        {
            ThrowError(x => x.DeliveryType, "Invalid delivery type");
        }

        return TypedResults.Ok(
            new CreateSessionResponse(sessionId));
    }
}