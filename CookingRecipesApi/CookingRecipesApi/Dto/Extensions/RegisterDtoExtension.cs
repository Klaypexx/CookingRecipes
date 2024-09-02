using Application.Auth.Entities;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipesDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class RegisterDtoExtension
{
    public static Register ToRegister( this RegisterDto registerDto )
    {
        return new()
        {
            Name = registerDto.Name,
            UserName = registerDto.UserName,
            Password = registerDto.Password,
        };
    }
}
