using Application.RecipesTags.Repositories;
using Domain.Recipes.Entities;

namespace Application.RecipesTags.Services;

public class RecipeTagService : IRecipeTagService
{
    private readonly IRecipeTagRepository _recipeTagRepository;
    public RecipeTagService( IRecipeTagRepository recipeTagRepository )
    {
        _recipeTagRepository = recipeTagRepository;
    }
    public async Task<List<RecipeTag>> GetRecipeTagsByRecipeIdAndTagIds( int recipeId, List<int> tagId )
    {
        return await _recipeTagRepository.GetRecipeTagsByRecipeIdAndTagIds( recipeId, tagId );
    }

    public async Task RemoveConnections( int recipeId, List<int> tagsId )
    {
        List<RecipeTag> recipeTags = await GetRecipeTagsByRecipeIdAndTagIds( recipeId, tagsId );

        _recipeTagRepository.RemoveConnections( recipeTags );
    }
}
