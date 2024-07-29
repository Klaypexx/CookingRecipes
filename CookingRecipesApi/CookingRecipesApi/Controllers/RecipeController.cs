using Application.Foundation;
using Application.Recipes.Services;
using CookingRecipesApi.Dto;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Utilities;
using Domain.Recipes.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> CreateRecipe( [FromBody] RecipeDto recipeDto )
    {
        Recipe recipe = new Recipe
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            Avatar = recipeDto.Avatar,
            CookingTime = recipeDto.CookingTime,
            Portion = recipeDto.Portion,
            AuthorId = int.Parse( User.GetUserId() ),
            Ingredients = recipeDto.Ingredients.Select( ingredientDto => new Ingredient
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipeDto.Steps.Select( stepDto => new Step
            {
                Description = stepDto.Description,
            } ).ToList(),
            Tags = await _recipeService.GetOrCreateTag( recipeDto.Tags.Select( tagDto => tagDto.Name ).ToList() )

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
        List<Recipe> recipes = await _recipeService.GetAllUserRecipes( userId );
        var recipeDto = recipes.Select( a => a.ToDto() ).ToList();
        return Ok( recipeDto );
    }
}
