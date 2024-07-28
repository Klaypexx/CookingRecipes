using Application.Foundation;
using Application.Recipes.Services;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipeDto;
using CookingRecipesApi.Utilities;
using Domain.Recipes.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "recipes" )]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeService _recipeService;
    public RecipeController( IUnitOfWork unitOfWork, IRecipeService recipeService )
    {
        _unitOfWork = unitOfWork;
        _recipeService = recipeService;
    }

    [HttpPost]
    [Route( "create" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromBody] RecipeDto body )
    {
        Recipe recipe = new Recipe
        {
            Name = body.Name,
            Description = body.Description,
            Avatar = body.Avatar,
            CookingTime = body.CookingTime,
            Portion = body.Portion,
            AuthorId = int.Parse( User.GetUserId() ),
        };

        await _recipeService.CreateRcipe( recipe );
        return Ok();
    }

    [HttpGet]
    [Route( "getall" )]
    [Authorize]
    public async Task<IActionResult> GetAllUserRecipes()
    {
        int userId = int.Parse( User.GetUserId() );
        List<Recipe> recipe = await _recipeService.GetAllUserRecipes( userId );
        return Ok( recipe );
    }
}
