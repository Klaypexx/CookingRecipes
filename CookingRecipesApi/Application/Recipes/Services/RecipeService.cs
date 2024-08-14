using Application.Recipes.Repositories;
using Application.Recipes.Utils;
using Application.Tags.Services;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Foundation;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRecipe( RecipeApplication recipe, string rootPath )
    {

        string avatarGuid = await AvatarService.CreateAvatar( recipe, rootPath );
        RecipeDomain recipeDomain = recipe.Create( avatarGuid );
        await _tagService.ActualizeTags( recipeDomain );
        await _tagService.CreatingLinksWithTags( recipeDomain );

        await _recipeRepository.CreateRecipe( recipeDomain );

        await _unitOfWork.Save();
    }

    public async Task UpdateRecipe( RecipeApplication actualRecipe, int recipeId, string rootPath )
    {
        RecipeDomain oldRecipe = await _recipeRepository.GetByIdWithAllDetails( recipeId );
        string avatarGuid = await AvatarService.UpdateAvatar( actualRecipe, oldRecipe, rootPath );
        RecipeDomain actualDomainRecipe = actualRecipe.Create( avatarGuid );

        await _tagService.ActualizeTags( actualDomainRecipe );
        await _tagService.CreatingLinksWithTags( actualDomainRecipe );

        oldRecipe.UpdateRecipe( actualDomainRecipe );
        await _unitOfWork.Save();

        await _tagService.RemoveUnusedTags();

    }

    public async Task RemoveRecipe( int recipeId, string rootPath )
    {
        RecipeDomain recipe = await GetByIdWithTag( recipeId );

        await _tagService.RemoveTagsLinks( recipe );
        await _tagService.RemoveUnusedTags();

        AvatarService.RemoveAvatar( recipe, rootPath );

        _recipeRepository.RemoveRecipe( recipe );

        await _unitOfWork.Save();
    }

    public async Task<List<RecipeDomain>> GetRecipesForPage( int skipRange )
    {
        return await _recipeRepository.GetRecipesForPage( skipRange );
    }

    public async Task<RecipeDomain> GetByIdWithAllDetails( int recipeId )
    {
        return await _recipeRepository.GetByIdWithAllDetails( recipeId );
    }

    public async Task<RecipeDomain> GetByIdWithTag( int recipeId )
    {
        return await _recipeRepository.GetByIdWithTag( recipeId );
    }

    public async Task<RecipeDomain> GetById( int recipeId )
    {
        return await _recipeRepository.GetById( recipeId );
    }

    public async Task<bool> HasAccessToRecipe( int recipeId, int authorId )
    {
        RecipeDomain recipe = await GetById( recipeId );

        return recipe.AuthorId == authorId;
    }
}
