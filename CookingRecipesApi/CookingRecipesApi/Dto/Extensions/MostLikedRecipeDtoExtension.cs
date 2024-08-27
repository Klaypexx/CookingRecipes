using Application.Recipes.Entities;
using CookingRecipesApi.Dto.RecipesDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class MostLikedRecipeDtoExtension
{
    public static MostLikedRecipeDto ToMostLikedRecipeDto( this MostLikedRecipe recipe )
    {
        return new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            AvatarPath = recipe.AvatarPath,
            AuthorName = recipe.AuthorName,
            LikeCount = recipe.LikeCount,
        };
    }
}
