using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Common.Abstractions;

public interface IUsersRepository
{
    Task AddAsync(User user);
    void Update(User user);

    Task<User?> GetAsync(Specification<User> specification);

    Task<bool> AnyAsync(Specification<User> specification);

    Task<UserProfile?> GetProfileAsync(Specification<UserProfile> specification);
}