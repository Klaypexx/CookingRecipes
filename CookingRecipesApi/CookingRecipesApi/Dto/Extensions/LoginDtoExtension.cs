using Application.Auth.Entities;
using CookingRecipesApi.Dto.AuthDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class LoginDtoExtension
{
    public static Login ToLogin( this LoginDto loginDto )
    {
        return new()
        {
            UserName = loginDto.UserName,
            Password = loginDto.Password,
        };
    }
}
