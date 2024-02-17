using Workshopper.Domain.Sessions;

namespace Workshopper.Tests.Common.Sessions;

public static class SessionFactory
{
    public static OnlineSession CreateOnlineSession(
        string? title = null,
        string? description = null,
        SessionType? sessionType = null,
        DateTimeOffset? startDateTime = null,
        DateTimeOffset? endDateTime = null,
        int? places = null,
        Guid? hostProfileId = null,
        string? link = null,
        Guid? id = null)
    {
        var onlineSession = OnlineSession.Create(
            title ?? Constants.Constants.OnlineSession.Title,
            description ?? Constants.Constants.OnlineSession.Description,
            sessionType ?? Constants.Constants.OnlineSession.SessionType,
            startDateTime ?? Constants.Constants.OnlineSession.StartDateTime,
            endDateTime ?? Constants.Constants.OnlineSession.EndDateTime,
            places ?? Constants.Constants.OnlineSession.Places,
            hostProfileId ?? Constants.Constants.UserProfiles.Id,
            link ?? Constants.Constants.OnlineSession.Link,
            id ?? Constants.Constants.OnlineSession.Id);

        return onlineSession;
    }
}