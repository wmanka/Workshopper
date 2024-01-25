using Microsoft.AspNetCore.Builder;
using Workshopper.Infrastructure.Common.Middleware;

namespace Workshopper.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();

        return app;
    }
}