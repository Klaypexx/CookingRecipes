using Application.Recipes.Entities;
using Application.Recipes.Services;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "recipes" )]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IValidator<RecipeDto> _recipeDtoValidator;
    public RecipeController( IRecipeService recipeService, IValidator<RecipeDto> recipeDtoValidator )
    {
        _recipeService = recipeService;
        _recipeDtoValidator = recipeDtoValidator;
    }

    [HttpPost]
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {
        ValidationResult validationResult = await _recipeDtoValidator.ValidateAsync( recipeDto );

        if ( !validationResult.IsValid )
        {
            return BadRequest( new ErrorResponse( validationResult.ToDictionary() ) );
        }

        try
        {
            int authorId = int.Parse( User.GetUserId() );

            await _recipeService.CreateRecipe( recipeDto.ToApplication( authorId ) );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpPut]
    [Route( "{recipeId}" )]
    [Authorize]
    public async Task<IActionResult> UpdateRecipe( [FromForm] RecipeDto recipeDto, [FromRoute] int recipeId )
    {
        int authorId = int.Parse( User.GetUserId() );

        bool hasAccess = await _recipeService.HasAccessToRecipe( recipeId, authorId );

        if ( !hasAccess )
        {
            return StatusCode( 403, new ErrorResponse( "Нет доступа" ) );
        }

        try
        {
            await _recipeService.UpdateRecipe( recipeDto.ToApplication( authorId ), recipeId );

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
        int authorId = int.Parse( User.GetUserId() );

        bool hasAccess = await _recipeService.HasAccessToRecipe( recipeId, authorId );

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
                authorId = int.Parse( User.GetUserId() );
            }

            IReadOnlyList<OverviewRecipe> recipes = await _recipeService.GetRecipes( pageNumber, authorId, searchString );
            IReadOnlyList<OverviewRecipeDto> recipesDto = recipes.ToOverviewRecipeDto();
            return Ok( recipesDto );
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
            IReadOnlyList<OverviewRecipe> recipes = await _recipeService.GetFavouriteRecipes( pageNumber, authorId );
            IReadOnlyList<OverviewRecipeDto> recipesDto = recipes.ToOverviewRecipeDto();
            return Ok( recipesDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }

    [HttpGet]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> GetRecipeById( [FromRoute] int recipeId )
    {
        try
        {
            int authorId = 0;
            if ( User.Identity.IsAuthenticated )
            {
                authorId = int.Parse( User.GetUserId() );
            }

            CompleteRecipe recipes = await _recipeService.GetRecipeById( recipeId, authorId );
            CompletetRecipeDto recipeDto = recipes.ToCompleteRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }
}
