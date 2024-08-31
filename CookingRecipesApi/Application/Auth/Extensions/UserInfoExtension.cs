using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth.Extensions;

public static class UserInfoExtension
{
    public static UserInfo ToUserInfo( this UserDomain user )
    {
        return new()
        {
            Name = user.Name,
            UserName = user.UserName,
            Description = user.Description,
        };
    }
}