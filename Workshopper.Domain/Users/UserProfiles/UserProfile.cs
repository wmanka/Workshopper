using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions;

namespace Workshopper.Domain.Users.UserProfiles;

public abstract class UserProfile : DomainEntity
{
    public ProfileType ProfileType { get; protected init; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public User User { get; set; }

    public Guid UserId { get; private set; }

    protected UserProfile(
        string firstName,
        string lastName,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
    }

    private UserProfile()
        : this(default!, default!)
    {
    }
}