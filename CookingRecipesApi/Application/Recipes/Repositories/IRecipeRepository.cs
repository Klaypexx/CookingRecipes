﻿using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;
public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    void UpdateRecipe( Recipe recipe );
    void RemoveRecipe( Recipe recipe );
    Task<List<Recipe>> GetRecipes( int skipRange );
    Task<Recipe> GetByIdWithAllDetails( int recipeId );
    Task<Recipe> GetByIdWithTag( int recipeId );
    Task<Recipe> GetById( int recipeId );
}
