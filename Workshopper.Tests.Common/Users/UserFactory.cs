﻿using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Tests.Common.Users;

public static class UserFactory
{
    public static User CreateUser(
        string? email = null,
        string? hash = null,
        Guid? id = null,
        HostProfile? hostProfile = null)
    {
        var user = User.Create(
            email: email ?? Common.Constants.Constants.Users.Email,
            hash: hash ?? Common.Constants.Constants.Users.Hash,
            id: id ?? Common.Constants.Constants.Users.Id);

        if (hostProfile is not null)
        {
            user.CreateHostProfile(hostProfile.FirstName, hostProfile.LastName, hostProfile.Title, hostProfile.Company, hostProfile.Bio);
        }

        return user;
    }
}