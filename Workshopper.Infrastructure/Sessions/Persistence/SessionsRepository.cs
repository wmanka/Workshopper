using Microsoft.EntityFrameworkCore;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Common.Models;
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

    public async Task<Session?> GetAsync(Guid id)
    {
        return await _context.Sessions.FindAsync(id);
    }

    public async Task<Session?> GetAsync(Specification<Session> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Sessions, specification).FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Specification<Session> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Sessions, specification).AnyAsync();
    }
}