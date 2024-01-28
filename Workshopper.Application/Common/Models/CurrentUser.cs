using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Common.Models;

public record CurrentUser
{
    private readonly ProfileType? _profileType;
    private readonly List<string> _roles = [];

    public CurrentUser(
        Guid userId,
        Guid? profileId,
        ProfileType? profileType,
        List<string> roles)
    {
        UserId = userId;
        ProfileId = profileId;
        _profileType = profileType;
        _roles = roles;
    }

    public Guid? ProfileId { get; }

    public Guid UserId { get; }

    public bool IsHost => _profileType == ProfileType.Host && IsInRole(DomainRoles.Host);

    public bool IsAttendee => _profileType == ProfileType.Attendee && IsInRole(DomainRoles.Attendee);

    private bool IsInRole(string role)
    {
        return _roles.Contains(role);
    }
}