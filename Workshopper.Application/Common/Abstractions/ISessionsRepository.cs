using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface ISessionsRepository
{
    public void UpdateSession(Session session);
    public Task<Session?> GetAsync(Guid id);
    public Task<bool> AnyAsync(Specification<Session> specification);
    public Task<Session?> GetAsync(Specification<Session> specification);
}