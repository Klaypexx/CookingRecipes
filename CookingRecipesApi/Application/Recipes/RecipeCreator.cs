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
            Ingredients = recipe.Ingredients.Select( ingredient => new Ingredient( ingredient.Name, ingredient.Product ) ).ToList(),
            Steps = recipe.Steps.Select( step => new Step( step.Description ) ).ToList(),
            Tags = recipe.Tags.Select( tag => new RecipeTag( new Tag( tag.Name ) ) ).ToList()
        };
    }
}
