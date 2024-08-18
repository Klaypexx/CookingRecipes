using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;

public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    void UpdateRecipe( Recipe recipe );
    void RemoveRecipe( Recipe recipe );
    Task<List<Recipe>> GetRecipes( int skipRange, int pageAmount );
    Task<Recipe> GetRecipeById( int recipeId );
    Task<Recipe> GetByIdWithTag( int recipeId );
    Task<Recipe> GetById( int recipeId );
}
