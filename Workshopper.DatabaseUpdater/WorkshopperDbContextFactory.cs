using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.DatabaseUpdater;

/// <remarks>Needed when the context is in class library project that's being referenced by other services</remarks>
public class WorkshopperDbContextFactory : IDesignTimeDbContextFactory<WorkshopperDbContext>
{
    public WorkshopperDbContext CreateDbContext(string[] args)
    {
        var opts = new DbContextOptionsBuilder<WorkshopperDbContext>();
        opts.LogTo(Console.WriteLine);

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory());

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

        var configuration = configurationBuilder.Build();

        var databaseOptions = configuration.GetRequiredSection(DatabaseOptions.SectionName)
            .Get<DatabaseOptions>();

        var options = Options.Create(databaseOptions!);
        var httpContextAccessor = new HttpContextAccessor();

        return new WorkshopperDbContext(opts.Options, options, httpContextAccessor);
    }
}