using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Entities;

namespace Application.Recipes.Extensions;

public static class CompleteRecipeExtension
{
    public static CompleteRecipe ToCompleteRecipe( this RecipeDomain recipe, int authorId )
    {
        return new()
        {
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
            } ).ToList(),
            Ingredients = recipe.Ingredients.Select( ingredientDto => new Ingredient
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipe.Steps.Select( stepDto => new Step
            {
                Description = stepDto.Description,
            } ).ToList(),
        };
    }
}
