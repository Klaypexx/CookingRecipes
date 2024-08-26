using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;

public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    void UpdateRecipe( Recipe recipe );
    void RemoveRecipe( Recipe recipe );
    Task<IReadOnlyList<Recipe>> GetRecipes( int skipRange, int pageAmount, string searchString );
    Task<IReadOnlyList<Recipe>> GetFavouriteRecipes( int skipRange, int pageAmount, int authorId );
    Task<Recipe> GetRecipeById( int recipeId );
    Task<Recipe> GetById( int recipeId );
}
