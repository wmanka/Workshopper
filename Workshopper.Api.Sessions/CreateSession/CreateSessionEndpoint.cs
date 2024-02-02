using ConfigCat.Client;
using Workshopper.Api.Sessions.Contracts;
using Workshopper.Api.Sessions.Contracts.CreateSession;
using Workshopper.Application.Common.Models;
using Workshopper.Application.Sessions.Commands.CreateSession;
using DomainAddress = Workshopper.Domain.Common.Address;
using DomainDeliveryType = Workshopper.Domain.Sessions.DeliveryType;
using DomainSessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateSessionEndpoint : Endpoint<CreateSessionRequest, CreateSessionResponse>
{
    private readonly IConfigCatClient _configCatClient;
    public CreateSessionEndpoint(IConfigCatClient configCatClient)
    {
        _configCatClient = configCatClient;
    }
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
                null,
                new Address("1 Main Street", "Apt 2", "London", "UK", "SW1A 1AA"));

            s.ExampleRequest = new CreateSessionRequest(
                DeliveryType.Online,
                SessionType.Discussion,
                "Git Basics Online",
                null,
                DateTimeOffset.Now.AddHours(1),
                DateTimeOffset.Now.AddHours(2),
                2,
                "https://zoom.us/j/1234567890?pwd=QWERTYUIOPASDFGHJKLZXCVBNM");

            s.ResponseExamples[200] = new CreateSessionResponse(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken ct)
    {
        // var x = await _configCatClient.GetValueAsync("notifications-enabled", false, cancellationToken: ct);

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