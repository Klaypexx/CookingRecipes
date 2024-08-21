﻿using Application.Recipes.Entities;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task CreateRecipe( Recipe recipe );
    Task UpdateRecipe( Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId );
    Task<IReadOnlyList<OverviewRecipe>> GetRecipes( int pageNumber, int authorId );
    Task<CompleteRecipe> GetRecipeById( int recipeId, int authorId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
