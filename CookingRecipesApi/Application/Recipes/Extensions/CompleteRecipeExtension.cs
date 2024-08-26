using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Entities;

namespace Application.Recipes.Extensions;

public static class CompleteRecipeExtension
{
    public static CompleteRecipe ToCompleteRecipe( this RecipeDomain recipe, bool isRecipeLiked )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            IsLike = isRecipeLiked,
            LikeCount = recipe.Likes.Count,
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
