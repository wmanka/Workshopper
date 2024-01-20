namespace Workshopper.Domain.Sessions;

public class SessionType(string name, int value) : SmartEnum<SessionType>(name, value)
{
    public readonly static SessionType Workshop = new(nameof(Workshop), 0);
    public readonly static SessionType Lecture = new(nameof(Lecture), 1);
    public readonly static SessionType Discussion = new(nameof(Discussion), 2);
}