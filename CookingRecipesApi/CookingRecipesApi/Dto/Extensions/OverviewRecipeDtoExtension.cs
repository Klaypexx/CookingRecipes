using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto.Extensions;

public static class OverviewRecipeDtoExtension
{
    public static OverviewRecipeDto ToOverviewRecipeDto( this Recipe recipe )
    {
        return new OverviewRecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            Tags = recipe.Tags.Select( recipeTag => new TagDto
            {
                Name = recipeTag.Tag.Name
            } ).ToList()
        };
    }

    public static IReadOnlyList<OverviewRecipeDto> ToOverviewRecipeDto( this IReadOnlyList<Recipe> recipes )
    {
        return recipes.Select( recipe => recipe.ToOverviewRecipeDto() ).ToList();
    }
}