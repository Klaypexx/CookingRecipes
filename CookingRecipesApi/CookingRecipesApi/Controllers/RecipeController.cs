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
    IWebHostEnvironment _appEnvironment;
    public RecipeController( IUnitOfWork unitOfWork, IRecipeService recipeService, ITagService tagService, IWebHostEnvironment appEnvironment )
    {
        _unitOfWork = unitOfWork;
        _recipeService = recipeService;
        _tagService = tagService;
        _appEnvironment = appEnvironment;
    }

    [HttpPost]
    [Route( "create" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {

        int authorId = int.Parse( User.GetUserId() );

        string imagePath = Path.Combine( "images", recipeDto.Avatar.FileName );
        string fullPath = Path.Combine( _appEnvironment.WebRootPath, imagePath );

        using ( var fileStream = new FileStream( fullPath, FileMode.Create ) )
        {
            await recipeDto.Avatar.CopyToAsync( fileStream );
        }

        List<RecipeTag> Tags = await _tagService.GetOrCreateTag( recipeDto.Tags.Select( tagDto => tagDto.Name ).ToList() );

        await _recipeService.CreateRcipe( recipeDto.ToDomain( authorId, Tags ) );
        return Ok( recipeDto );
    }

    [HttpPost]
    [Route( "get" )]
    [Authorize]
    public async Task<IActionResult> GetAllRecipes( [FromBody] int page = 1 )
    {
        List<Recipe> recipes = await _recipeService.GetAllRecipes( page );
        var recipeDto = recipes.Select( a => a.ToDto() ).ToList();
        return Ok( recipeDto );
    }
}
