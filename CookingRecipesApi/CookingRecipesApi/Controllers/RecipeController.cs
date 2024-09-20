using Application.Recipes.Entities;
using Application.Recipes.Facade;
using Application.ResultObject;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Extensions;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "recipes" )]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeFacade _recipeFacade;

    private int AuthorId => int.Parse( User.GetUserId() );

    public RecipeController( IRecipeFacade recipeFacade )
    {
        _recipeFacade = recipeFacade;
    }

    [HttpPost]
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {
        Result result = await _recipeFacade.CreateRecipe( recipeDto.ToApplication( AuthorId ) );

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
        Result result = await _recipeFacade.UpdateRecipe( recipeDto.ToApplication( AuthorId ), recipeId );

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
        Result result = await _recipeFacade.RemoveRecipe( recipeId, AuthorId );

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

        Result<RecipesData<OverviewRecipe>> result = await _recipeFacade.GetRecipes( authorId, pageNumber, searchString );

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
        Result<RecipesData<OverviewRecipe>> result = await _recipeFacade.GetFavouriteRecipeByAuthorId( AuthorId, pageNumber );

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
        Result<RecipesData<OverviewRecipe>> result = await _recipeFacade.GetRecipeByAuthorId( AuthorId, pageNumber );

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
        Result<MostLikedRecipe> result = await _recipeFacade.GetMostLikedRecipe();

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
            authorId = AuthorId;
        }

        Result<CompleteRecipe> result = await _recipeFacade.GetRecipeByIdIncludingDependentEntities( recipeId, authorId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        CompleteRecipeDto recipeDto = result.Value.ToCompleteRecipeDto();

        return Ok( recipeDto );
    }
}
