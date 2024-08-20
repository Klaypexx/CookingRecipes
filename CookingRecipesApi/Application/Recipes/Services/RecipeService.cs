using Application.Files.Services;
using Application.Foundation;
using Application.Recipes.Repositories;
using Application.Tags.Services;
using Domain.Recipes.Entities;

namespace Application.Recipes.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IFileService _fileService;
    private readonly IRecipeCreator _recipeCreator;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IFileService fileService,
        IRecipeCreator recipeCreator,
        IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _fileService = fileService;
        _recipeCreator = recipeCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRecipe( Entities.Recipe recipe )
    {
        string pathToFile = await _fileService.SaveImage( recipe.Avatar );
        Recipe recipeDomain = _recipeCreator.Create( recipe, pathToFile );

        await _tagService.ActualizeTags( recipeDomain );
        await _recipeRepository.CreateRecipe( recipeDomain );
        await _unitOfWork.Save();
    }

    public async Task UpdateRecipe( Entities.Recipe actualRecipe, int recipeId )
    {
        Recipe oldRecipe = await _recipeRepository.GetRecipeById( recipeId );
        string pathToFile = await _fileService.UpdateImage( actualRecipe.Avatar, oldRecipe.Avatar );
        Recipe recipe = _recipeCreator.Create( actualRecipe, pathToFile );

        await _tagService.ActualizeTags( recipe );
        oldRecipe.UpdateRecipe( recipe );
        await _unitOfWork.Save();

        await RemoveUnusedTags();
    }

    public async Task RemoveRecipe( int recipeId )
    {
        Recipe recipe = await _recipeRepository.GetByIdWithTag( recipeId );

        _fileService.RemoveImage( recipe.Avatar );

        recipe.Tags.Clear();
        _recipeRepository.RemoveRecipe( recipe );
        await _unitOfWork.Save();

        await RemoveUnusedTags();
    }

    private async Task RemoveUnusedTags()
    {
        await _tagService.RemoveUnusedTags();
        await _unitOfWork.Save();
    }

    public async Task<List<Recipe>> GetRecipes( int skipRange, int pageAmount )
    {
        return await _recipeRepository.GetRecipes( skipRange, pageAmount );
    }

    public async Task<Recipe> GetRecipeById( int recipeId )
    {
        return await _recipeRepository.GetRecipeById( recipeId );
    }

    public async Task<bool> HasAccessToRecipe( int recipeId, int authorId )
    {
        Recipe recipe = await _recipeRepository.GetById( recipeId );

        return recipe.AuthorId == authorId;
    }
}
