using Microsoft.EntityFrameworkCore;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Users.Persistence;

internal class UsersRepository : IUsersRepository
{
    private readonly WorkshopperDbContext _context;

    public UsersRepository(WorkshopperDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public async Task<User?> GetAsync(Specification<User> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Users, specification)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Specification<User> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Users, specification)
            .AnyAsync();
    }

    public async Task<UserProfile?> GetProfileAsync(Specification<UserProfile> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.UserProfiles, specification)
            .FirstOrDefaultAsync();
    }
}