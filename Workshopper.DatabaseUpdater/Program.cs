using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Workshopper.DatabaseUpdater;
using Workshopper.Infrastructure.Common.Persistence;

var services = new ServiceCollection();

services.AddLogging();
services.AddDbContext<WorkshopperDbContext>();
services.AddScoped(typeof(IDesignTimeDbContextFactory<WorkshopperDbContext>), typeof(WorkshopperDbContextFactory));
services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();

var serviceProvider = services.BuildServiceProvider();
var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

using var scope = serviceScopeFactory.CreateScope();
var scopedServiceProvider = scope.ServiceProvider;

var logger = scopedServiceProvider.GetRequiredService<ILogger<Program>>();

var context = (WorkshopperDbContextFactory)scopedServiceProvider
    .GetRequiredService(typeof(IDesignTimeDbContextFactory<WorkshopperDbContext>));

logger.LogInformation("--------------STARTED MIGRATING EXPENSER CONTEXT---------");

var ctx = context.CreateDbContext(Array.Empty<string>());
ctx.Database.Migrate();

logger.LogInformation("--------------FINISHED MIGRATING EXPENSER CONTEXT---------");