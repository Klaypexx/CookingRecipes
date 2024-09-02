using Application.Users.Entities;
using CookingRecipesApi.Dto.UsersDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class UserDtoExtension
{
    public static User ToApplication( this UserDto userDto )
    {
        return new()
        {
            Name = userDto.Name,
            UserName = userDto.UserName,
            Description = userDto.Description,
            Password = userDto.Password,
        };
    }
}