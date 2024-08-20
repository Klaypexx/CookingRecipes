using Application.Recipes.Entities;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes;

public interface IRecipeCreator
{
    RecipeDomain Create( Recipe recipe, string pathToFile );
}
