using Domain.Recipes.Entities;

namespace Application.Recipes;

public class RecipeCreator : IRecipeCreator
{
    public Recipe Create( Entities.Recipe recipe, string pathToFile )
    {
        return new Recipe( recipe.Name,
            recipe.Description,
            recipe.CookingTime,
            recipe.Portion,
            pathToFile,
            recipe.AuthorId,
            recipe.Ingredients.Select( ingredient => new Ingredient( ingredient.Name, ingredient.Product ) ).ToList(),
            recipe.Steps.Select( step => new Step( step.Description ) ).ToList(),
            recipe.Tags.Select( tag => new RecipeTag( new Tag( tag.Name ) ) ).ToList()
        );
    }
}
