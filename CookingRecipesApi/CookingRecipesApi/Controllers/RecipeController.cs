using Application.Foundation;
using Application.Recipes.Services;
using Application.Tags.Services;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Dto.Validators;
using CookingRecipesApi.Utilities;
using Domain.Auth.Entities;
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
    [Authorize]
    public async Task<IActionResult> CreateRecipe( [FromForm] RecipeDto recipeDto )
    {

        ValidationResult validationResult = await _recipeDtoValidator.ValidateAsync( recipeDto );

        if ( !validationResult.IsValid )
        {
            return BadRequest( new { message = validationResult.ToString() } );
        }

        try
        {
            if ( recipeDto.Avatar != null )
            {
                string imagePath = Path.Combine( "images", recipeDto.Avatar.FileName );
                string fullPath = Path.Combine( _appEnvironment.WebRootPath, imagePath );

                using ( var fileStream = new FileStream( fullPath, FileMode.Create ) )
                {
                    await recipeDto.Avatar.CopyToAsync( fileStream );
                }
            }

            int authorId = int.Parse( User.GetUserId() );

            List<RecipeTag> Tags = await _tagService.GetTags( recipeDto.Tags?.Select( tagDto => tagDto.Name ).ToList() );

            await _recipeService.CreateRcipe( recipeDto.ToDomain( authorId, Tags ) );
        }
        catch ( Exception exception )
        {
            return BadRequest( new { message = exception.Message } );
        }

        return Ok();
    }

    [HttpGet]
    [Route( "get" )]
    [Authorize]
    public async Task<IActionResult> GetAllRecipes( [FromHeader] int page = 1, int pageAmount = 4 )
    {
        try
        {
            int skipRange = ( page - 1 ) * pageAmount;
            List<Recipe> recipes = await _recipeService.GetAllRecipes( skipRange );
            List<CardRecipeDto> recipeDto = recipes.Select( a => a.ToCardRecipeDto() ).ToList();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new { message = exception.Message } );
        }

    }

    [HttpGet]
    [Route( "get/{recipeId}" )]
    [Authorize]
    public async Task<IActionResult> GetByIdWithAllDetails( [FromHeader] int recipeId )
    {
        try
        {
            Recipe recipes = await _recipeService.GetByIdWithAllDetails( recipeId );
            var recipeDto = recipes.ToCurrentRecipeDto();
            return Ok( recipeDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new { message = exception.Message } );
        }

    }
}
