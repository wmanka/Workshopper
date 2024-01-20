using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class OnlineSessionsRepository : IOnlineSessionsRepository
{
    private readonly WorkshopperDbContext _context;

    public OnlineSessionsRepository(WorkshopperDbContext context)
    {
        _context = context;
    }

    public async Task AddSessionAsync(OnlineSession subscription)
    {
        await _context.OnlineSessions.AddAsync(subscription);
    }
}