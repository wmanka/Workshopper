using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Authentication;

namespace Workshopper.Api.Common.Tests.Unit;

public class CurrentUserProviderTests
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CurrentUserProvider _currentUserProvider;

    public CurrentUserProviderTests()
    {
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _currentUserProvider = new CurrentUserProvider(_httpContextAccessor);
    }

    [Fact]
    public void GetCurrentUser_ReturnCurrentUser_WhenCurrentUserExists()
    {
        var userId = Guid.NewGuid();
        _httpContextAccessor.Configure().HttpContext.Returns(new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(CustomClaimType.UserId, userId.ToString())
            }))
        });

        var result = _currentUserProvider.GetCurrentUser();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new
        {
            UserId = userId
        });
    }

    [Fact]
    public void GetCurrentUser_ReturnNull_WhenCurrentUserDoesnNotExists()
    {
        _httpContextAccessor.Configure().HttpContext.ReturnsNull();

        var result = _currentUserProvider.GetCurrentUser();

        result.Should().BeNull();
    }

    [Fact]
    public void GetCurrentUser_ReturnCurrentUserWithProfileData_WhenCurrentUserIsLoggedInSpecificProfile()
    {
        var userId = Guid.NewGuid();
        var profileId = Guid.NewGuid();
        var profileType = ProfileType.Host;
        const string role = DomainRoles.Host;
        _httpContextAccessor.Configure().HttpContext.Returns(new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(CustomClaimType.UserId, userId.ToString()),
                new Claim(CustomClaimType.ProfileId, profileId.ToString()),
                new Claim(CustomClaimType.ProfileType, profileType.Name),
                new Claim(CustomClaimType.Role, role)
            }))
        });

        var result = _currentUserProvider.GetCurrentUser();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new
        {
            UserId = userId,
            ProfileId = profileId
        });

        result!.IsHost.Should().BeTrue();
    }
}