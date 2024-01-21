using Workshopper.Api.Sessions.Contracts.Sessions;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;

namespace Workshopper.Api.Sessions.CreateSession;

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
        var deliveryType = DomainDeliveryType.FromName(request.DeliveryType.ToString());

        var sessionId = Guid.Empty;
        if (deliveryType == DomainDeliveryType.Online)
        {
            var command = CreateSessionMapper.ToCreateOnlineSessionCommand(request);
            sessionId = await command.ExecuteAsync(ct);
        }
        else if (deliveryType == DomainDeliveryType.Stationary)
        {
            var command = CreateSessionMapper.ToCreateStationarySessionCommand(request);
            sessionId = await command.ExecuteAsync(ct);
        }

        await SendOkAsync(
            new CreateSessionResponse(sessionId),
            ct);
    }
}