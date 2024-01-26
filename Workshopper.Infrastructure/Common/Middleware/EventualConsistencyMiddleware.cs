using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Workshopper.Domain.Common;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware()
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EventualConsistencyMiddleware> _logger;

    public EventualConsistencyMiddleware(
        RequestDelegate next,
        ILogger<EventualConsistencyMiddleware> logger) : this()
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, WorkshopperDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();

        httpContext.Response.OnCompleted(async () =>
        {
            try
            {
                if (httpContext.Items.TryGetValue("DomainEventsQueue", out var value)
                    && value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        // await domainEvent.PublishAsync(); // todo why it doesn't work
                        await (domainEvent as IEvent).PublishAsync();
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while saving changes");
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(httpContext);
    }
}