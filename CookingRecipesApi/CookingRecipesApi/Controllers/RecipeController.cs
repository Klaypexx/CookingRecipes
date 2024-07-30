using Application.Foundation;
using Application.Recipes.Services;
using Application.Tags.Services;
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
    private readonly ITagService _tagService;
    public RecipeController( IUnitOfWork unitOfWork, IRecipeService recipeService, ITagService tagService )
    {
        _unitOfWork = unitOfWork;
        _recipeService = recipeService;
        _tagService = tagService;
    }

    [HttpPost]
    [Route( "create" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {

        int authorId = int.Parse( User.GetUserId() );
       /* List<RecipeTag> Tags = await _tagService.GetOrCreateTag( recipeDto.Tags.Select( tagDto => tagDto.Name ).ToList() );*/

        /*    await _recipeService.CreateRcipe( recipeDto.ToDomain( authorId, Tags ) );*/
        return Ok( recipeDto );
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
