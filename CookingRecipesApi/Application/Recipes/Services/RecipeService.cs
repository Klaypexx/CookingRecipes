using Application.Recipes.Repositories;
using Application.Tags.Services;
using Domain.Recipes.Entities;
using Application.Foundation;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IRecipeCreator _recipeCreator;
    private readonly IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IRecipeCreator recipeCreator,
        IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _recipeCreator = recipeCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRecipe( Entities.Recipe recipe, string rootPath )
    {

        string avatarGuid = await AvatarService.CreateAvatar( recipe.Avatar, rootPath );
        Recipe recipeDomain = _recipeCreator.Create( recipe, avatarGuid );

        await _tagService.ActualizeTags( recipeDomain );
        await _recipeRepository.CreateRecipe( recipeDomain );
        await _unitOfWork.Save();
    }

    public async Task UpdateRecipe( Entities.Recipe actualRecipe, int recipeId, string rootPath )
    {
        Recipe oldRecipe = await _recipeRepository.GetByIdWithAllDetails( recipeId );
        string avatarGuid = await AvatarService.UpdateAvatar( actualRecipe.Avatar, oldRecipe.Avatar, rootPath );
        Recipe actualDomainRecipe = _recipeCreator.Create( actualRecipe, avatarGuid );

        await _tagService.ActualizeTags( actualDomainRecipe );
        oldRecipe.UpdateRecipe( actualDomainRecipe );
        await _unitOfWork.Save();

        await _tagService.RemoveUnusedTags();
        await _unitOfWork.Save();

    }

    public async Task RemoveRecipe( int recipeId, string rootPath )
    {
        Recipe recipe = await _recipeRepository.GetByIdWithTag( recipeId );

        AvatarService.RemoveAvatar( recipe.Avatar, rootPath );

        _tagService.RemoveTagsLinks( recipe );
        await _tagService.RemoveUnusedTags();
        await _unitOfWork.Save();

        _recipeRepository.RemoveRecipe( recipe );
        await _unitOfWork.Save();
    }

    public async Task<List<Recipe>> GetRecipesForPage( int skipRange )
    {
        return await _recipeRepository.GetRecipesForPage( skipRange );
    }

    public async Task<Recipe> GetByIdWithAllDetails( int recipeId )
    {
        return await _recipeRepository.GetByIdWithAllDetails( recipeId );
    }

    public async Task<bool> HasAccessToRecipe( int recipeId, int authorId )
    {
        Recipe recipe = await _recipeRepository.GetById( recipeId );

        return recipe.AuthorId == authorId;
    }
}
