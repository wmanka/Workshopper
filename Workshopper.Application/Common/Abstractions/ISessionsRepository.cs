using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface ISessionsRepository
{
    public void UpdateSession(Session session);
    public Task<Session?> GetSessionAsync(Guid id);
    public Task<bool> AnyAsync(Specification<Session> specification);
}