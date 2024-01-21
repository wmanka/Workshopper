using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Interfaces;

public interface ISessionsRepository
{
    public void UpdateSession(Session session);
    public Task<Session?> GetSessionAsync(Guid id);
    public Task<bool> AnyAsync(Specification<Session> specification);
}