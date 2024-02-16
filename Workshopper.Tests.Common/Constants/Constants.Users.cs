namespace Workshopper.Tests.Common.Constants;

public static partial class Constants
{
    public static class UserProfile
    {
        public const string Email = "john@email.com";
        public const string Hash = "hash";
        public readonly static Guid Id = Guid.NewGuid();
    }
}