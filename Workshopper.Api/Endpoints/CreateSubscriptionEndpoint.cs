using Workshopper.Contracts.Subscriptions;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

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
        await Task.CompletedTask;

        // return TypedResults.NotFound();

        // AddError(r => r.SubscriptionType, "not null");
        // return new ProblemDetails(ValidationFailures);

        return TypedResults.Ok(new CreateSubscriptionResponse(Guid.NewGuid(), SubscriptionType.Starter));
    }
}