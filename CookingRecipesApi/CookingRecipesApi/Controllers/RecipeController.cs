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
[Authorize]
public class RecipeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeService _recipeService;
    private readonly ITagService _tagService;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IValidator<RecipeDto> _recipeDtoValidator;
    public RecipeController( IUnitOfWork unitOfWork,
        IRecipeService recipeService,
        ITagService tagService,
        IWebHostEnvironment appEnvironment,
        IValidator<RecipeDto> recipeDtoValidator )
    {
        _unitOfWork = unitOfWork;
        _recipeService = recipeService;
        _tagService = tagService;
        _appEnvironment = appEnvironment;
        _recipeDtoValidator = recipeDtoValidator;
    }

    [HttpPost]
    [Route( "create" )]
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

            await _unitOfWork.Save();

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

    }

    [HttpDelete]
    [Route( "delete/{recipeId}" )]
    public async Task<IActionResult> RemoveRecipe( [FromRoute] int recipeId )
    {
        try
        {
            await _recipeService.RemoveRecipe( recipeId );

            await _unitOfWork.Save();

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "get/list/{pageNumber}" )]
    public async Task<IActionResult> GetRecipesForPage( [FromRoute] int pageNumber = 1, int pageAmount = 4 )
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
    [Route( "get/{recipeId}" )]
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
