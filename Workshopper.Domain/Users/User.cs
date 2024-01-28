using Workshopper.Domain.Common;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Users;

public class User : DomainEntity
{

    public User(
        string email,
        string hash,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Email = email;
        Hash = hash;
        UserSettings = new UserSettings(Id);
    }

    public string Email { get; private set; }

    public string Hash { get; private set; }

    public HostProfile? HostProfile { get; private set; }

    public Guid? HostProfileId { get; private set; }

    public AttendeeProfile? AttendeeProfile { get; private set; }

    public Guid? AttendeeProfileId { get; private set; }

    public UserSettings UserSettings { get; }

    public void CreateHostProfile(
        string firstName,
        string lastName,
        string? title,
        string? company,
        string? bio)
    {
        if (HostProfile is not null)
        {
            throw new DomainException(UserErrors.ProfileAlreadyExists);
        }

        HostProfile = new HostProfile(firstName, lastName, title, company, bio);
    }

    private User()
    {
    }
}