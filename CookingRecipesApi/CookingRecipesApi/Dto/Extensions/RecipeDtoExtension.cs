using Application.Recipes.Entities;
using CookingRecipesApi.Dto.RecipesDto;

namespace CookingRecipesApi.Dto.Extensions;

public static class RecipeDtoExtension
{
    public static Recipe ToDomain( this RecipeDto recipeDto, int authorId )
    {
        return new()
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            CookingTime = recipeDto.CookingTime,
            Portion = recipeDto.Portion,
            AuthorId = authorId,
            Avatar = recipeDto.Avatar,
            Ingredients = recipeDto.Ingredients.Select( ingredientDto => new Ingredient
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipeDto.Steps.Select( stepDto => new Step
            {
                Description = stepDto.Description,
            } ).ToList(),
            Tags = recipeDto.Tags.Select( tagDto => new Tag
            {
                Name = tagDto.Name,
            } ).ToList(),
        };
    }
}
