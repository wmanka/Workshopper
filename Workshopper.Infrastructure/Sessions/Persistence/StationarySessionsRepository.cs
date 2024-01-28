using Workshopper.Application.Common.Abstractions;
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

    public async Task AddStationarySessionAsync(StationarySession stationarySession)
    {
        await _context.StationarySessions.AddAsync(stationarySession);
    }
}