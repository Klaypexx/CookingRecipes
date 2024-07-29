using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto;
public static class RecipeDtoExtension
{
    public static RecipeDto ToDto( this Recipe recipe )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            Avatar = recipe.Avatar,
            Ingredients = recipe.Ingredients.Select( ingredientDto => new IngredientDto
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipe.Steps.Select( stepDto => new StepDto
            {
                Description = stepDto.Description,
            } ).ToList()

        };
    }
}
