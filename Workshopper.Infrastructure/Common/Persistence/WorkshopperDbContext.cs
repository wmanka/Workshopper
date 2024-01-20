using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Subscriptions;

namespace Workshopper.Infrastructure.Common.Persistence;

public class WorkshopperDbContext : DbContext, IUnitOfWork
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public DbSet<StationarySession> StationarySessions { get; set; } = null!;

    public DbSet<OnlineSession> OnlineSessions { get; set; } = null!;

    public WorkshopperDbContext(
        DbContextOptions<WorkshopperDbContext> options,
        IOptions<DatabaseOptions> databaseOptions)
        : base(options)
    {
        _databaseOptions = databaseOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_databaseOptions.Value.ConnectionString,
                opt =>
                {
                    opt.MigrationsAssembly(typeof(WorkshopperDbContext).Assembly.FullName);
                    opt.MigrationsHistoryTable("schema_version", DatabaseSchema.Public);
                })
#if DEBUG
            .LogTo(Console.WriteLine)
            .EnableDetailedErrors()
#endif
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Public);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkshopperDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}