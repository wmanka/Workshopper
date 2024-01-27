using Workshopper.Domain.Users;

namespace Workshopper.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task AddAsync(User user);

    Task<User?> GetAsync(Specification<User> specification);

    Task<bool> AnyAsync(Specification<User> specification);
}