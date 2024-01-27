using Workshopper.Domain.Common;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Users;

public sealed class UserSettings : DomainEntity
{
    public ProfileType? DefaultProfileType { get; private set; }

    public UserSettings(
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
    }

    private UserSettings()
        : this(default!)
    {
    }
}