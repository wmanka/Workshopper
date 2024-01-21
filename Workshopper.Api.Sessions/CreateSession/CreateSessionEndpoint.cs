using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands;
using Workshopper.Application.Sessions.Commands.CreateSession;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
        Validator<CreateSessionValidator>();
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
        var command = CreateSessionCommandFactory.CreateSessionCommand(
            DomainDeliveryType.FromName(request.DeliveryType.ToString()),
            DomainSessionType.FromName(request.SessionType.ToString()),
            request.Title,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Places,
            request.Link,
            request.Address);

        var sessionId = await command.ExecuteAsync(ct);

        await SendOkAsync(
            new CreateSessionResponse(sessionId),
            ct);
    }
}