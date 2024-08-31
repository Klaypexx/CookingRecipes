using Application.Auth.Entities;
using CookingRecipesApi.Dto.AuthDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class UserInfoDtoExtension
{
    public static UserInfoDto ToUserInfoDto( this UserInfo user )
    {
        return new()
        {
            Name = user.Name,
            UserName = user.UserName,
            Description = user.Description,
        };
    }
}