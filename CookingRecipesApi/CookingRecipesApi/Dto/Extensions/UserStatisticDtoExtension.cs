using Application.Auth.Entities;
using CookingRecipesApi.Dto.AuthDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class UserStatisticDtoExtension
{
    public static UserStatisticDto ToUserStatisticDto( this UserStatistic user )
    {
        return new()
        {
            RecipesCount = user.RecipesCount,
            LikeCount = user.LikeCount,
            FavouritesCount = user.FavouritesCount,
        };
    }
}