using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Specifications;

internal class UserByIdSpecification : Specification<User>
{
    public UserByIdSpecification(Guid id)
    {
        AddFilter(session => session.Id == id);
    }
}