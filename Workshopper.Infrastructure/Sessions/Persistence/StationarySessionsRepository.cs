using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class StationarySessionsRepository : IStationarySessionsRepository
{
    private readonly WorkshopperDbContext _context;

    public StationarySessionsRepository(WorkshopperDbContext context)
    {
        _context = context;
    }

    public async Task AddSessionAsync(StationarySession subscription)
    {
        await _context.StationarySessions.AddAsync(subscription);
    }
}