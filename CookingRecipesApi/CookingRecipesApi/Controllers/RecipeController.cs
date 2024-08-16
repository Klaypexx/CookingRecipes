using Application.Foundation;
using Application.Recipes.Services;
using Application.Tags.Services;
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
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IValidator<RecipeDto> _recipeDtoValidator;
    public RecipeController( IUnitOfWork unitOfWork,
        IRecipeService recipeService,
        ITagService tagService,
        IWebHostEnvironment appEnvironment,
        IValidator<RecipeDto> recipeDtoValidator )
    {
        _recipeService = recipeService;
        _appEnvironment = appEnvironment;
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

            await _recipeService.CreateRecipe( recipeDto.ToDomain( authorId ), _appEnvironment.WebRootPath );

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
            await _recipeService.UpdateRecipe( recipeDto.ToDomain( authorId ), recipeId, _appEnvironment.WebRootPath );

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
            await _recipeService.RemoveRecipe( recipeId, _appEnvironment.WebRootPath );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "" )]
    public async Task<IActionResult> GetRecipesForPage( [FromQuery] int pageNumber = 1, int pageAmount = 4 )
    {
        try
        {
            int skipRange = ( pageNumber - 1 ) * pageAmount;
            List<Recipe> recipes = await _recipeService.GetRecipesForPage( skipRange );
            List<CardRecipeDto> recipeDto = recipes.Select( a => a.ToCardRecipeDto() ).ToList();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }

    [HttpGet]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> GetByIdWithAllDetails( [FromRoute] int recipeId )
    {
        try
        {
            Recipe recipes = await _recipeService.GetByIdWithAllDetails( recipeId );
            CurrentRecipeDto recipeDto = recipes.ToCurrentRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }
}
