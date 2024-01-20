namespace Workshopper.Domain.Sessions;

public class Level(string name, int value) : SmartEnum<Level>(name, value)
{
    public readonly static Level Foundational = new(nameof(Foundational), 0);
    public readonly static Level Intermediate = new(nameof(Intermediate), 1);
    public readonly static Level Advanced = new(nameof(Advanced), 2);
    public readonly static Level Expert = new(nameof(Expert), 3);
}