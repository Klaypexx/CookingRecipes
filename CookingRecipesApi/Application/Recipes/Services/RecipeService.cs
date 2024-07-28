using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Repositories;
using Application.Foundation;
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

    public async Task<List<Recipe>> GetAllUserRecipes( int userId )
    {
        return await _recipeRepository.GetAllUserRecipes( userId );
    }
}
