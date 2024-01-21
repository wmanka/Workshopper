using Workshopper.Api.Contracts.Subscriptions;
using Workshopper.Application.Subscriptions.Commands.CreateSubscription;
using DomainSubscriptionType = Workshopper.Domain.Subscriptions.SubscriptionType;

namespace Workshopper.Api.Subscriptions;

public class CreateSubscriptionEndpoint : Endpoint<CreateSubscriptionRequest, CreateSubscriptionResponse>
{
    public override void Configure()
    {
        Post("/subscriptions");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create a subscription";
            s.ExampleRequest = new CreateSubscriptionRequest("Standard subscription", SubscriptionType.Standard);
            s.ResponseExamples[200] = new CreateSubscriptionResponse(Guid.NewGuid());
        });
    }

    public override async Task HandleAsync(CreateSubscriptionRequest req, CancellationToken ct)
    {
        var command = new CreateSubscriptionCommand
        {
            Name = req.Name,
            SubscriptionType = DomainSubscriptionType.FromName(req.SubscriptionType.ToString()),
        };

        var subscriptionId = await command.ExecuteAsync(ct);

        await SendCreatedAtAsync("/subscriptions/{id}",
            new
            {
                Id = subscriptionId
            },
            new CreateSubscriptionResponse(subscriptionId),
            cancellation: ct);
    }
}