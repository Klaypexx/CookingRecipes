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
            string avatarGuid = null;

            if ( recipeDto.Avatar != null )
            {
                avatarGuid = Guid.NewGuid().ToString() + recipeDto.Avatar.FileName;
                string imagePath = Path.Combine( "images", avatarGuid );
                string fullPath = Path.Combine( _appEnvironment.WebRootPath, imagePath );
                using FileStream fileStream = new( fullPath, FileMode.Create );

                await recipeDto.Avatar.CopyToAsync( fileStream );
            }

            int authorId = int.Parse( User.GetUserId() );

            List<RecipeTag> tags = await _tagService.GetTags(
                recipeDto.Tags?.Select( tagDto => tagDto.Name ).ToList()
            );

            // 1 создать вспомогательный класс, в него сохранить все что есть в дто (на уровне application)
            // 2 создать из дто + authorId объект вспомогательного класса
            // 3 в CreateRcipe прокинуть вспомогательный класс и путь до папки сохранения
            // 4 на уровне application получить все данные для создания доменной сущности 
            // 5 создать доменную сущность (посмотреть порождающие паттерны) можно создать сервис RecipeCreator и в нем всего 1 метод create()
            await _recipeService.CreateRcipe( recipeDto.ToDomain( authorId, tags, avatarGuid ) );


            await _unitOfWork.Save();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

        return Ok();
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
