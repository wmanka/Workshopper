namespace Workshopper.Domain.Users.UserProfiles;

public class ProfileType(string name, int value) : SmartEnum<ProfileType>(name, value)
{
    public readonly static ProfileType Attendee = new(nameof(Attendee), 0);
    public readonly static ProfileType Host = new(nameof(Host), 1);
}