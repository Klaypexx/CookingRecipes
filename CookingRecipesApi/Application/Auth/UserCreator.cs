using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth;

public class UserCreator : IUserCreator
{
    public UserDomain Create( User user )
    {
        return new UserDomain( user.Name, user.UserName, user.Description, user.Password );
    }
}
