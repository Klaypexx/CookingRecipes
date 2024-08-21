using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Entities;

namespace Application.Recipes.Extensions;

public static class OverviewRecipeExtension
{
    public static OverviewRecipe ToOverviewRecipe( this RecipeDomain recipe, IReadOnlyList<int> likedIds )
    {
        return new OverviewRecipe
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            IsLike = likedIds.Contains( recipe.Id ),
            Tags = recipe.Tags.Select( recipeTag => new Tag
            {
                Name = recipeTag.Tag.Name
            } ).ToList()
        };
    }

    public static IReadOnlyList<OverviewRecipe> ToOverviewRecipe( this IReadOnlyList<RecipeDomain> recipes, IReadOnlyList<int> likedIds )
    {
        return recipes.Select( recipe => recipe.ToOverviewRecipe( likedIds ) ).ToList();
    }
}
