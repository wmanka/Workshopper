using Workshopper.Domain.Sessions;

namespace Workshopper.Domain.Users.UserProfiles;

public class HostProfile : UserProfile
{
    private readonly List<Session> _hostedSessions = [];

    public HostProfile(
        string firstName,
        string lastName,
        string? title = null,
        string? company = null,
        string? bio = null,
        Guid? id = null)
        : base(
            firstName,
            lastName,
            id)
    {
        ProfileType = ProfileType.Host;
        Title = title;
        Company = company;
        Bio = bio;
        IsVerified = false;
    }

    public string? Title { get; private set; }

    public string? Company { get; private set; }

    public string? Bio { get; private set; }

    public bool IsVerified { get; private set; }

    public IReadOnlyCollection<Session> HostedSessions => _hostedSessions.AsReadOnly();

    public void Verify()
    {
        IsVerified = true;
    }

    private HostProfile()
        : this(default!, default!, default!)
    {
    }
}