using Application.Tags.Repositories;
using Domain.Recipes.Entities;

namespace Application.Tags.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    public TagService( ITagRepository tagRepository )
    {
        _tagRepository = tagRepository;
    }

    public async Task ActualizeTags( Recipe recipe )
    {
        List<string> actualTagsNames = recipe.Tags
            .Select( recipeTag => recipeTag.Tag.Name.ToLower() )
            .ToList();

        if ( actualTagsNames.Count != 0 )
        {
            List<RecipeTag> existingRecipesTags = await _tagRepository.GetRecipesTagsByTagsNames( actualTagsNames );

            List<RecipeTag> recipeTagsToCreate = actualTagsNames
                .Where( name => !existingRecipesTags.Any( tag => tag.Tag.Name.ToLower() == name ) )
                .Select( name => new RecipeTag( new Tag( name ) ) )
                .ToList();

            List<RecipeTag> existingRecipesTagsToAdd = existingRecipesTags.Select( recipeTag => new RecipeTag( recipeTag.TagId, recipeTag.Tag ) ).ToList();

            recipe.SetTags( [ .. existingRecipesTagsToAdd, .. recipeTagsToCreate ] );

            List<Tag> tagsToCreate = recipeTagsToCreate.Select( recipeTag => recipeTag.Tag ).ToList();

            await _tagRepository.CreateTags( tagsToCreate );
        }
    }

    public async Task RemoveUnusedTags()
    {
        List<Tag> tags = await _tagRepository.GetAllTagsWithRecipeTags();

        List<Tag> tagsToRemove = tags.Where( tag => tag.Recipes.Count == 0 ).ToList();

        _tagRepository.RemoveTags( tagsToRemove );
    }
}
