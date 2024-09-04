using Application.Recipes.Entities;
using Application.Recipes.Services;
using Application.ResultObject;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "recipes" )]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    private int AuthorId => int.Parse( User.GetUserId() );

    public RecipeController( IRecipeService recipeService )
    {
        _recipeService = recipeService;
    }

    [HttpPost]
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {
        Result result = await _recipeService.CreateRecipe( recipeDto.ToApplication( AuthorId ) );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpPut]
    [Route( "{recipeId}" )]
    [Authorize]
    public async Task<IActionResult> UpdateRecipe( [FromForm] RecipeDto recipeDto, [FromRoute] int recipeId )
    {
        bool hasAccess = await _recipeService.HasAccessToRecipe( recipeId, AuthorId );

        if ( !hasAccess )
        {
            return StatusCode( 403, new ErrorResponse( "Нет доступа" ) );
        }

        try
        {
            await _recipeService.UpdateRecipe( recipeDto.ToApplication( AuthorId ), recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpDelete]
    [Route( "{recipeId}" )]
    [Authorize]
    public async Task<IActionResult> RemoveRecipe( [FromRoute] int recipeId )
    {
        bool hasAccess = await _recipeService.HasAccessToRecipe( recipeId, AuthorId );

        if ( !hasAccess )
        {
            return StatusCode( 403, new ErrorResponse( "Нет доступа" ) );
        }

        try
        {
            await _recipeService.RemoveRecipe( recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "" )]
    public async Task<IActionResult> GetRecipes( [FromQuery] int pageNumber = 1, [FromQuery] string searchString = "" )
    {
        try
        {
            int authorId = 0;
            if ( User.Identity.IsAuthenticated )
            {
                authorId = AuthorId;
            }

            RecipesData<OverviewRecipe> recipesData = await _recipeService.GetRecipes( pageNumber, authorId, searchString );
            RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( recipesData.Recipes.ToOverviewRecipeDto(), recipesData.IsLastRecipes );
            return Ok( recipesDtoData );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "favourites" )]
    [Authorize]
    public async Task<IActionResult> GetFavouritesRecipes( [FromQuery] int pageNumber = 1 )
    {
        try
        {
            int authorId = int.Parse( User.GetUserId() );
            RecipesData<OverviewRecipe> recipesData = await _recipeService.GetFavouriteRecipes( pageNumber, authorId );
            RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( recipesData.Recipes.ToOverviewRecipeDto(), recipesData.IsLastRecipes );
            return Ok( recipesDtoData );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "userRecipes" )]
    [Authorize]
    public async Task<IActionResult> GetUserRecipes( [FromQuery] int pageNumber = 1 )
    {
        try
        {
            int authorId = int.Parse( User.GetUserId() );
            RecipesData<OverviewRecipe> recipesData = await _recipeService.GetUserRecipes( pageNumber, authorId );
            RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( recipesData.Recipes.ToOverviewRecipeDto(), recipesData.IsLastRecipes );
            return Ok( recipesDtoData );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "liked" )]
    public async Task<IActionResult> GetMostLikedRecipe()
    {
        try
        {
            MostLikedRecipe recipe = await _recipeService.GetMostLikedRecipe();

            if ( recipe == null )
            {
                return Ok();
            }

            MostLikedRecipeDto recipeDto = recipe.ToMostLikedRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> GetRecipeByIdIncludingDependentEntities( [FromRoute] int recipeId )
    {
        try
        {
            int authorId = 0;
            if ( User.Identity.IsAuthenticated )
            {
                authorId = int.Parse( User.GetUserId() );
            }

            CompleteRecipe recipes = await _recipeService.GetRecipeByIdIncludingDependentEntities( recipeId, authorId );
            CompletetRecipeDto recipeDto = recipes.ToCompleteRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }
}
