using Application.Recipes.Entities;
using CookingRecipesApi.Dto.RecipesDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class OverviewRecipeDtoExtension
{
    public static OverviewRecipeDto ToOverviewRecipeDto( this OverviewRecipe recipe )
    {
        return new OverviewRecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.AvatarPath,
            AuthorName = recipe.AuthorName,
            IsLike = recipe.IsLike,
            Tags = recipe.Tags.Select( recipeTag => new TagDto
            {
                Name = recipeTag.Name
            } ).ToList()
        };
    }

    public static IReadOnlyList<OverviewRecipeDto> ToOverviewRecipeDto( this IReadOnlyList<OverviewRecipe> recipes )
    {
        return recipes.Select( recipe => recipe.ToOverviewRecipeDto() ).ToList();
    }
}