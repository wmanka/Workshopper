using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands.CreateOnlineSession;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.Endpoints;

public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse>
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
                12,
                "https://zoom.us/j/1234567890?pwd=QWERTYUIOPASDFGHJKLZXCVBNM",
                null);

            s.ResponseExamples[200] = new CreateSessionResponse(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken ct)
    {
        if (!DomainDeliveryType.TryFromName(request.DeliveryType.ToString(), out var deliveryType))
        {
            ThrowError(x => x.DeliveryType, "Invalid delivery type");
        }

        if (!DomainSessionType.TryFromName(request.SessionType.ToString(), out var sessionType))
        {
            ThrowError(x => x.SessionType, "Invalid session type");
        }

        var sessionId = Guid.Empty;
        if (deliveryType == DomainDeliveryType.Online)
        {
            sessionId = await new CreateOnlineSessionCommand
            {
                SessionType = sessionType,
                Title = request.Title,
                Description = request.Description,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                Places = request.Places,
                Link = request.Link!
            }.ExecuteAsync(ct);
        }
        else if (deliveryType == DomainDeliveryType.Stationary)
        {
        }

        await SendOkAsync(
            new CreateSessionResponse(sessionId),
            ct);
    }
}