using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth;

public interface IUserCreator
{
    UserDomain Create( User user );
}
