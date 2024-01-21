using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Interfaces;

public interface IOnlineSessionsRepository
{
    public Task AddOnlineSessionAsync(OnlineSession onlineSession);
}