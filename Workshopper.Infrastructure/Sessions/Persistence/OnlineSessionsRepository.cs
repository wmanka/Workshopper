using Workshopper.Application.Common.Abstractions;
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

    public async Task AddOnlineSessionAsync(OnlineSession onlineSession)
    {
        await _context.OnlineSessions.AddAsync(onlineSession);
    }
}