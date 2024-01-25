﻿using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Subscriptions;

namespace Workshopper.Infrastructure.Common.Persistence;

public class WorkshopperDbContext : DbContext, IUnitOfWork
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public DbSet<Session> Sessions { get; set; } = null!;

    public DbSet<StationarySession> StationarySessions { get; set; } = null!;

    public DbSet<OnlineSession> OnlineSessions { get; set; } = null!;

    public DbSet<HybridSession> HybridSessions { get; set; } = null!;

    public WorkshopperDbContext(
        DbContextOptions<WorkshopperDbContext> options,
        IOptions<DatabaseOptions> databaseOptions,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _databaseOptions = databaseOptions;
        _httpContextAccessor = httpContextAccessor;
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
        var domainEvents = ChangeTracker.Entries<DomainEntity>()
            .Select(x => x.Entity.ApplyDomainEvents())
            .SelectMany(x => x)
            .ToList();

        if (IsUserWaitingOnline())
        {
            AddDomainEventsToQueue(domainEvents);
        }
        else
        {
            await PublishDomainEvents(domainEvents);
        }

        await base.SaveChangesAsync();
    }

    private async static Task PublishDomainEvents(List<IEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await domainEvent.PublishAsync();
        }
    }

    private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

    private void AddDomainEventsToQueue(List<IEvent> domainEvents)
    {
        var domainEventsQueue = _httpContextAccessor.HttpContext!.Items.TryGetValue("DomainEventsQueue", out var value)
                                && value is Queue<IEvent> queue
            ? queue
            : new Queue<IEvent>();

        domainEvents.ForEach(domainEventsQueue.Enqueue);

        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
}