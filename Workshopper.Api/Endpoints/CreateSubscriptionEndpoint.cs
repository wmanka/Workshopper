﻿using Workshopper.Contracts.Subscriptions;
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
        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Create a subscription";
            s.ExampleRequest = new CreateSubscriptionRequest(SubscriptionType.Pro, Guid.NewGuid());
            s.ResponseExamples[200] = new CreateSubscriptionResponse(Guid.NewGuid(), SubscriptionType.Pro);
        });
    }

    public override async Task<Results<Ok<CreateSubscriptionResponse>, NotFound, ProblemDetails>> ExecuteAsync(
        CreateSubscriptionRequest req, CancellationToken ct)
    {
        var subscriptionId = await new CreateSubscriptionCommand
        {
            SubscriptionType = req.SubscriptionType.ToString(),
            AdminId = req.AdminId
        }.ExecuteAsync(ct);

        return TypedResults.Ok(
            new CreateSubscriptionResponse(subscriptionId, req.SubscriptionType));
    }
}