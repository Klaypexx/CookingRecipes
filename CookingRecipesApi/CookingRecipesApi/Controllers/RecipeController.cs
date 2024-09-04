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
        Result result = await _recipeService.UpdateRecipe( recipeDto.ToApplication( AuthorId ), recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();

    }

    [HttpDelete]
    [Route( "{recipeId}" )]
    [Authorize]
    public async Task<IActionResult> RemoveRecipe( [FromRoute] int recipeId )
    {
        Result result = await _recipeService.RemoveRecipe( recipeId, AuthorId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpGet]
    [Route( "" )]
    public async Task<IActionResult> GetRecipes( [FromQuery] int pageNumber = 1, [FromQuery] string searchString = "" )
    {
        int authorId = 0;
        if ( User.Identity.IsAuthenticated )
        {
            authorId = AuthorId;
        }

        Result<RecipesData<OverviewRecipe>> result = await _recipeService.GetRecipes( pageNumber, authorId, searchString );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( result.Value.Recipes.ToOverviewRecipeDto(), result.Value.IsLastRecipes );

        return Ok( recipesDtoData );
    }

    [HttpGet]
    [Route( "favourites" )]
    [Authorize]
    public async Task<IActionResult> GetFavouritesRecipes( [FromQuery] int pageNumber = 1 )
    {
        Result<RecipesData<OverviewRecipe>> result = await _recipeService.GetFavouriteRecipes( pageNumber, AuthorId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( result.Value.Recipes.ToOverviewRecipeDto(), result.Value.IsLastRecipes );

        return Ok( recipesDtoData );
    }

    [HttpGet]
    [Route( "userRecipes" )]
    [Authorize]
    public async Task<IActionResult> GetUserRecipes( [FromQuery] int pageNumber = 1 )
    {
        Result<RecipesData<OverviewRecipe>> result = await _recipeService.GetUserRecipes( pageNumber, AuthorId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        RecipesDataDto<OverviewRecipeDto> recipesDtoData = new( result.Value.Recipes.ToOverviewRecipeDto(), result.Value.IsLastRecipes );

        return Ok( recipesDtoData );
    }

    [HttpGet]
    [Route( "liked" )]
    public async Task<IActionResult> GetMostLikedRecipe()
    {
        Result<MostLikedRecipe> result = await _recipeService.GetMostLikedRecipe();

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        if ( result.Value == null )
        {
            return Ok();
        }

        MostLikedRecipeDto recipeDto = result.Value.ToMostLikedRecipeDto();

        return Ok( recipeDto );

    }

    [HttpGet]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> GetRecipeByIdIncludingDependentEntities( [FromRoute] int recipeId )
    {
        int authorId = 0;
        if ( User.Identity.IsAuthenticated )
        {
            authorId = int.Parse( User.GetUserId() );
        }

        Result<CompleteRecipe> result = await _recipeService.GetRecipeByIdIncludingDependentEntities( recipeId, authorId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        CompletetRecipeDto recipeDto = result.Value.ToCompleteRecipeDto();

        return Ok( recipeDto );

    }
}
