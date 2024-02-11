using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface IOnlineSessionsRepository
{
    Task AddOnlineSessionAsync(OnlineSession onlineSession);
}