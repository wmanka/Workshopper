using Microsoft.AspNetCore.SignalR;
using Workshopper.Infrastructure.Authentication;

namespace Workshopper.Infrastructure.Notifications;

/// <summary>
/// Defines from where signalR gets the userId when calling Clients.Users(userIds)
/// </summary>
public class UserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(CustomClaimType.UserId)?.Value!;
    }
}