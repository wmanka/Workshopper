using Workshopper.Api.Sessions.Contracts;
using Workshopper.Api.Sessions.Contracts.CreateSession;
using Workshopper.Application.Common.Roles;
using Workshopper.Application.Sessions.Commands.CreateSession;
using DomainAddress = Workshopper.Domain.Common.Address;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse>
{
    public override void Configure()
    {
        Post("/sessions");
        Roles(DomainRoles.Host);
        Validator<CreateSessionValidator>();

        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create a session";
            s.ExampleRequest = new CreateSessionRequest(
                DeliveryType.Stationary,
                SessionType.Workshop,
                "Git Basics",
                null,
                DateTimeOffset.Now.AddHours(1),
                DateTimeOffset.Now.AddHours(2),
                12,
                "https://zoom.us/j/1234567890?pwd=QWERTYUIOPASDFGHJKLZXCVBNM",
                new Address("1 Main Street", "Apt 2", "London", "UK", "SW1A 1AA"));

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
            request.Address != null
                ? new DomainAddress(
                    request.Address.Line1,
                    request.Address.Line2,
                    request.Address.City,
                    request.Address.Country,
                    request.Address.PostCode)
                : null);

        var sessionId = await command.ExecuteAsync(ct);

        await SendOkAsync(
            new CreateSessionResponse(sessionId),
            ct);
    }
}