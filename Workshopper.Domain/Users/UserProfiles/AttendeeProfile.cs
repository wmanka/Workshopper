using Workshopper.Domain.Sessions;

namespace Workshopper.Domain.Users.UserProfiles;

public class AttendeeProfile : UserProfile
{
    private readonly List<Session> _attendedSessions = [];

    public AttendeeProfile(
        string firstName,
        string lastName,
        Guid? id = null)
        : base(
            firstName,
            lastName,
            id)
    {
        ProfileType = ProfileType.Attendee;
    }

    public IReadOnlyCollection<Session> AttendedSessions => _attendedSessions.AsReadOnly();

    private AttendeeProfile()
        : this(default!, default!)
    {
    }
}