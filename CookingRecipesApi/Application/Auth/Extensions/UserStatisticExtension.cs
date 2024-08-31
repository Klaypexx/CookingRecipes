using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth.Extensions;

public static class UserStatisticExtension
{
    public static UserStatistic ToUserStatistic( this UserDomain user )
    {
        return new()
        {
            RecipesCount = user.Recipes.Count,
            LikesCount = user.Likes.Count,
            FavouritesCount = user.FavouriteRecipes.Count,
        };
    }
}