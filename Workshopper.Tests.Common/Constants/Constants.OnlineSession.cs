using Workshopper.Domain.Sessions;

namespace Workshopper.Tests.Common.Constants;

public static partial class Constants
{
    public static class OnlineSession
    {
        public const string Title = "Git Basics";
        public const string Description = "Session description...";
        public readonly static SessionType SessionType = SessionType.Discussion;
        public const string Link = "https://zoom.us/j/1234567890?pwd=QWERTYUIOPASDFGHJKLZXCVBNM";
        public readonly static DateTimeOffset StartDateTime = new DateTimeOffset(2025, 1, 1, 10, 0, 0, TimeSpan.Zero);
        public readonly static DateTimeOffset EndDateTime = new DateTimeOffset(2025, 1, 1, 11, 0, 0, TimeSpan.Zero);
        public const int Places = 10;
        public readonly static Guid Id = Guid.NewGuid();
    }
}