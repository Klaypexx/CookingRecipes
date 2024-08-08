using Application.Foundation;
using Application.Recipes.Repositories;
using Application.Recipes.Utils;
using Application.Tags.Services;
using Domain.Auth.Entities;
using Domain.Recipes.Entities;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    public RecipeService( IRecipeRepository recipeRepository, ITagService tagService )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
    }

    public async Task CreateRecipe( RecipeApplication recipe, string rootPath )
    {
        string avatarGuid = null;

        if ( recipe.Avatar != null )
        {
            avatarGuid = Guid.NewGuid().ToString() + recipe.Avatar.FileName;
            string imagePath = Path.Combine( "images", avatarGuid );
            string fullPath = Path.Combine( rootPath, imagePath );
            using FileStream fileStream = new( fullPath, FileMode.Create );

            await recipe.Avatar.CopyToAsync( fileStream );
        }

        List<RecipeTag> tags = null;

        if ( recipe.Tags != null )
        {
            tags = await _tagService.GetTags(
                recipe.Tags.Select( tag => tag.Name ).ToList()
            );
        }

        await _recipeRepository.CreateRecipe( recipe.Create( tags, avatarGuid ) );
    }

    public async Task RemoveRecipe( int recipeId )
    {
        RecipeDomain recipe = await GetByIdWithTag( recipeId );
        if ( recipe.Tags != null )
        {
            await _tagService.RemoveTags( recipeId );
        }
        _recipeRepository.RemoveRecipe( recipe );
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
}
