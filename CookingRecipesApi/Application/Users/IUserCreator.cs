﻿using Application.Auth.Entities;
using Application.Users.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users;

public interface IUserCreator
{
    UserDomain Create( User user, string hashedPassword );
    UserDomain Create( Register user, string hashedPassword );
}
