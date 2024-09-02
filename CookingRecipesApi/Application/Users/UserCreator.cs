﻿using Application.Auth.Entities;
using Application.Users.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users;

public class UserCreator : IUserCreator
{
    public UserDomain Create( User user, string hashedPassword )
    {
        return new UserDomain( user.Name, user.UserName, user.Description, hashedPassword );
    }

    public UserDomain Create( Register user, string hashedPassword )
    {
        return new UserDomain( user.Name, user.UserName, hashedPassword );
    }
}
