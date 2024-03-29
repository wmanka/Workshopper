﻿using Workshopper.Application.Common;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Specifications;

internal class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
    {
        AddFilter(session => session.Email == email);
    }
}