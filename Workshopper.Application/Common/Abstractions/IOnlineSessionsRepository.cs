using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface IOnlineSessionsRepository
{
    public Task AddOnlineSessionAsync(OnlineSession onlineSession);
}