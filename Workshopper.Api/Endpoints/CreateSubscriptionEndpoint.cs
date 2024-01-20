using Workshopper.Contracts.Subscriptions;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Workshopper.Application.Subscriptions.Commands.CreateSubscription;

namespace Workshopper.Api.Endpoints;

public class CreateSubscriptionEndpoint : Endpoint<CreateSubscriptionRequest,
    Results<Ok<CreateSubscriptionResponse>, NotFound, ProblemDetails>>
{
    public override void Configure()
    {
        Post("/subscriptions");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<CreateSubscriptionResponse>, NotFound, ProblemDetails>> ExecuteAsync(
        CreateSubscriptionRequest req, CancellationToken ct)
    {
        var subscriptionId = await new CreateSubscriptionCommand
        {
            SubscriptionType = req.SubscriptionType.ToString(),
            AdminId = req.AdminId
        }.ExecuteAsync(ct);

        // AddError(r => r.SubscriptionType, "not null");
        // return new ProblemDetails(ValidationFailures);

        return TypedResults.Ok(
            new CreateSubscriptionResponse(subscriptionId, req.SubscriptionType));
    }
}