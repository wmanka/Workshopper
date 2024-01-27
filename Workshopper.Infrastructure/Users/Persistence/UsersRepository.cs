using Microsoft.EntityFrameworkCore;
using Workshopper.Application.Common;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Users;
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

    public async Task<User?> GetAsync(Specification<User> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Users, specification).FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Specification<User> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Users, specification).AnyAsync();
    }
}