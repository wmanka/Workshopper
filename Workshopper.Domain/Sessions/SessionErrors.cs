namespace Workshopper.Domain.Sessions;

public static class SessionErrors
{
    public const string SessionAlreadyCanceled = "The session has already been canceled";
    public const string SessionAlreadyStarted = "The session has already started";
    public const string SessionTimeCollides = "The session time collides with another session";
}