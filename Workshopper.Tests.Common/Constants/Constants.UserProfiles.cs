namespace Workshopper.Tests.Common.Constants;

public static partial class Constants
{
    public static class UserProfiles
    {
        public const string FirstName = "John";
        public const string LastName = "Smith";
        public const string Title = "Software Engineer";
        public const string Company = "Microsoft";
        public const string Bio = "I am a software engineer.";
        public readonly static Guid Id = Guid.NewGuid();
    }
}