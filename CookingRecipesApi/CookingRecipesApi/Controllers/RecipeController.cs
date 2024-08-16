using Application.Recipes.Services;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Utilities;
using Domain.Recipes.Entities;
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

            await _recipeService.CreateRecipe( recipeDto.ToDomain( authorId ) );

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
            await _recipeService.UpdateRecipe( recipeDto.ToDomain( authorId ), recipeId );

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
    public async Task<IActionResult> GetRecipes( [FromQuery] int pageNumber = 1 )
    {
        try
        {
            int pageAmount = 4;
            int skipRange = ( pageNumber - 1 ) * pageAmount;
            List<Recipe> recipes = await _recipeService.GetRecipes( skipRange );
            IReadOnlyList<CardRecipeDto> recipeDto = recipes.ToCardRecipeDto();
            return Ok( recipeDto );
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
            Recipe recipes = await _recipeService.GetRecipeById( recipeId );
            CurrentRecipeDto recipeDto = recipes.ToCurrentRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }
}
