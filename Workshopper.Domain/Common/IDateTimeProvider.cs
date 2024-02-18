namespace Workshopper.Domain.Common;

public interface IDateTimeProvider
{
    public DateTimeOffset Now { get; }
}