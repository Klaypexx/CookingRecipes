using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Entities;

namespace Application.Recipes.Extensions;

public static class MostLikedRecipeExtension
{
    public static MostLikedRecipe ToMostLikedRecipe( this RecipeDomain recipe )
    {
        return new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            LikeCount = recipe.Likes.Count,
        };
    }
}
