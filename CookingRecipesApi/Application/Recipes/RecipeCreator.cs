using Domain.Recipes.Entities;
namespace Application.Recipes;
public class RecipeCreator : IRecipeCreator
{
    public Recipe Create( Entities.Recipe recipe, string pathToFile ) // вынести из утилс в папку с логикой рецепта
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Avatar = pathToFile,
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
