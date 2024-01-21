namespace Workshopper.Domain.Sessions;

public static class SessionErrors
{
    public const string SessionAlreadyCanceled = "The session has already been canceled";
    public const string SessionAlreadyStarted = "The session has already started";
    public const string SessionTimeOverlaps = "The session time overlaps with another session";
    public const string StartTimeMustBeGreaterThanNow = "Session must start in the future";
    public const string EndTimeMustBeGreaterThanStartTime = "Session end time must be greater than start time";
    public const string NumberOfPlacesMustBeGreaterThanZero = "Session must  have at least one place";
}