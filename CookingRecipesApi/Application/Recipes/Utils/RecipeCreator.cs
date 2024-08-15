using Domain.Recipes.Entities;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Utils;
public static class RecipeCreator
{
    public static RecipeDomain Create( this RecipeApplication recipe, string avatarGuid ) // вынести из утилс в папку с логикой рецепта
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Avatar = avatarGuid,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AuthorId = recipe.AuthorId,
            Ingredients = recipe.Ingredients.Select( ingredient => new Ingredient
            {
                Name = ingredient.Name,
                Product = ingredient.Product
            } ).ToList(),
            Steps = recipe.Steps.Select( step => new Step
            {
                Description = step.Description,
            } ).ToList(),
            Tags = recipe.Tags.Select( tag => new RecipeTag
            {
                Tag = new Tag { Name = tag.Name }
            } ).ToList()
        };
    }
}
