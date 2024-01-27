using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Sessions;

public class SessionAttendance
{
    public Guid SessionId { get; set; }

    public Session Session { get; set; }

    public Guid AttendeeProfileId { get; set; }

    public AttendeeProfile Attendee { get; set; }

    public bool IsCanceled { get; private set; }

    private SessionAttendance()
    {
        IsCanceled = false;
    }
}