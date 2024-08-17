using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto.Extensions;

public static class CompleteRecipeDtoExtension
{
    public static CompletetRecipeDto ToCompleteRecipeDto( this Recipe recipe )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            Tags = recipe.Tags.Select( recipeTag => new TagDto
            {
                Name = recipeTag.Tag.Name
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

