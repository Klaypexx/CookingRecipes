using Domain.Recipes.Entities;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Utils;
public static class RecipeCreator
{
    public static RecipeDomain Create( this RecipeApplication recipe, List<RecipeTag> tags, string avatarGuid )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Avatar = avatarGuid,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AuthorId = recipe.AuthorId,
            Ingredients = recipe.Ingredients.Select( ingredientDto => new Ingredient
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipe.Steps.Select( stepDto => new Step
            {
                Description = stepDto.Description,
            } ).ToList(),
            Tags = tags
        };
    }
}
