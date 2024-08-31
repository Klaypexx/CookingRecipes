using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth;

public class UserCreator : IUserCreator
{
    public UserDomain Create( User user, string hashedPassword )
    {
        return new UserDomain( user.Name, user.UserName, user.Description, hashedPassword );
    }
}
