using Application.Recipes.Entities;
using CookingRecipesApi.Dto.RecipesDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class CompleteRecipeDtoExtension
{
    public static CompletetRecipeDto ToCompleteRecipeDto( this CompleteRecipe recipe )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.AvatarPath,
            AuthorName = recipe.AuthorName,
            IsLike = recipe.IsLike,
            LikeCount = recipe.LikeCount,
            Tags = recipe.Tags.Select( recipeTag => new TagDto
            {
                Name = recipeTag.Name
            } ).ToList(),
            Ingredients = recipe.Ingredients.Select( ingredientDto => new IngredientDto
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipe.Steps.Select( stepDto => new StepDto
            {
                Description = stepDto.Description,
            } ).ToList(),
        };
    }
}

