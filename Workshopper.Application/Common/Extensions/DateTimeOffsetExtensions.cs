namespace Workshopper.Domain.Common;

public static class DateTimeOffsetExtensions
{
    public static bool DoTimeFramesOverlap(
        (DateTimeOffset startDateTime, DateTimeOffset endDateTime) firstTimeFrame,
        (DateTimeOffset startDateTime, DateTimeOffset endDateTime) secondTimeFrame)
    {
        return (firstTimeFrame.startDateTime <= secondTimeFrame.endDateTime && firstTimeFrame.startDateTime >= secondTimeFrame.startDateTime) ||
               (firstTimeFrame.endDateTime >= secondTimeFrame.startDateTime && firstTimeFrame.endDateTime <= secondTimeFrame.endDateTime);
    }
}