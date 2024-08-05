﻿using Application.Foundation;
using Application.Recipes.Repositories;
using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository, IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRcipe( Recipe recipe )
    {
        await _recipeRepository.CreateRecipe( recipe );
        await _unitOfWork.Save();
    }

    public async Task<List<Recipe>> GetAllRecipes( int skipRange )
    {
        return await _recipeRepository.GetAllRecipes( skipRange );
    }

    public async Task<Recipe> GetByIdWithAllDetails( int recipeId )
    {
        return await _recipeRepository.GetByIdWithAllDetails( recipeId );
    }
}
