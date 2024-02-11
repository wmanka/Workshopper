using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface ISessionsRepository
{
    void UpdateSession(Session session);
    Task<Session?> GetAsync(Guid id);
    Task<bool> AnyAsync(Specification<Session> specification);
    Task<Session?> GetAsync(Specification<Session> specification);
}