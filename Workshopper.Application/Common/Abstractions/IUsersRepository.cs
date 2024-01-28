using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Common.Abstractions;

public interface IUsersRepository
{
    Task AddAsync(User user);

    Task<User?> GetAsync(Specification<User> specification);

    Task<bool> AnyAsync(Specification<User> specification);
}