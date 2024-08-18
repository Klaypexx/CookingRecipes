﻿using Domain.Recipes.Entities;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task CreateRecipe( Entities.Recipe recipe );
    Task UpdateRecipe( Entities.Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId );
    Task<List<Recipe>> GetRecipes( int skipRange, int pageAmount );
    Task<Recipe> GetRecipeById( int recipeId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
