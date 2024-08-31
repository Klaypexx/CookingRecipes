using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Entities;

namespace Application.Recipes.Extensions;

public static class OverviewRecipeExtension
{
    public static OverviewRecipe ToOverviewRecipe( this RecipeDomain recipe, int authorId )
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
            IsLike = recipe.Likes.Any( like => like.UserId == authorId ),
            LikeCount = recipe.Likes.Count,
            IsFavourite = recipe.FavouriteRecipes.Any( favourite => favourite.UserId == authorId ),
            FavouriteCount = recipe.FavouriteRecipes.Count,
            Tags = recipe.Tags.Select( recipeTag => new Tag
            {
                Name = recipeTag.Tag.Name
            } ).ToList()
        };
    }

    public static IReadOnlyList<OverviewRecipe> ToOverviewRecipe( this IReadOnlyList<RecipeDomain> recipes, int authorId )
    {
        return recipes.Select( recipe => recipe.ToOverviewRecipe( authorId ) ).ToList();
    }
}
