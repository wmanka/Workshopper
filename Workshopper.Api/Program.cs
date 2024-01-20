using Workshopper.Application;
using Workshopper.Infrastructure;

var builder = WebApplication.CreateBuilder();
{
    builder.Services.AddFastEndpoints();

    builder.Services
        .AddApplication()
        .AddInfrastructure();
}

var app = builder.Build();
{
    app.UseFastEndpoints(x =>
    {
        x.Errors.UseProblemDetails();
    });

    app.Run();
}