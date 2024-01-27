using Workshopper.Application.Common;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Specifications;

internal class UserByEmailAndHashSpecification : Specification<User>
{
    public UserByEmailAndHashSpecification(string email, string hash)
    {
        AddFilter(user => user.Email == email
                          && user.Hash == hash);
    }
}