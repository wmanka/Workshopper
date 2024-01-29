using Workshopper.Domain.Common;
using Workshopper.Domain.Users.Events;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Users;

public sealed class User : DomainEntity
{
    private User(
        string email,
        string hash,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Email = email;
        Hash = hash;
    }

    public string Email { get; private set; }

    public string Hash { get; private set; }

    public HostProfile? HostProfile { get; private set; }

    public Guid? HostProfileId { get; private set; }

    public AttendeeProfile? AttendeeProfile { get; private set; }

    public Guid? AttendeeProfileId { get; private set; }

    public UserSettings UserSettings { get; private set; }

    public static User Create(
        string email,
        string hash,
        Guid? id = null)
    {
        var user = new User(email, hash, id);
        user.UserSettings = new UserSettings(user.Id);

        user._domainEvents.Add(new UserRegisteredDomainEvent(user));

        return user;
    }

    public void CreateHostProfile(
        string firstName,
        string lastName,
        string? title = null,
        string? company = null,
        string? bio = null)
    {
        if (HostProfile is not null)
        {
            throw new DomainException(UserErrors.UserProfileAlreadyExists);
        }

        HostProfile = HostProfile.Create(firstName, lastName, title, company, bio);
    }

    private User()
    {
    }
}