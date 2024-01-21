using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class SessionsRepository : ISessionsRepository
{
    private readonly WorkshopperDbContext _context;

    public SessionsRepository(WorkshopperDbContext context)
    {
        _context = context;
    }

    public void UpdateSession(Session session)
    {
        _context.Sessions.Update(session);
    }

    public async Task<Session?> GetSessionAsync(Guid id)
    {
        return await _context.Sessions.FindAsync(id);
    }
}