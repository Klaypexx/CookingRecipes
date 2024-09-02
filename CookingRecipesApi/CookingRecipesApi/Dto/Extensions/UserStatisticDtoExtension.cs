using Application.Users.Entities;
using CookingRecipesApi.Dto.UsersDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class UserStatisticDtoExtension
{
    public static UserStatisticDto ToUserStatisticDto( this UserStatistic user )
    {
        return new()
        {
            RecipesCount = user.RecipesCount,
            LikesCount = user.LikesCount,
            FavouritesCount = user.FavouritesCount,
        };
    }
}