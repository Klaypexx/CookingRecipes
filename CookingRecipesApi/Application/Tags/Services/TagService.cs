using Application.Foundation;
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
        List<string> actualTagsNames = recipe.Tags.Select( tag => tag.Tag.Name.ToLower() ).ToList();

        if ( actualTagsNames.Count != 0 )
        {
            List<Tag> existingTags = await _tagRepository.GetTagsByNames( actualTagsNames );

            List<RecipeTag> existingRecipeTagsToAdd = existingTags
                .Select( tag => new RecipeTag { TagId = tag.Id } )
                .ToList();

            List<RecipeTag> recipeTagsToCreate = actualTagsNames
                .Where( name => !existingTags.Any( tag => tag.Name.ToLower() == name ) )
                .Select( name => new RecipeTag { Tag = new Tag { Name = name } } )
                .ToList();

            recipe.Tags = [ .. existingRecipeTagsToAdd, .. recipeTagsToCreate ];

            await _tagRepository.CreateTags( recipeTagsToCreate.Select( recipeTag => recipeTag.Tag ).ToList() );
        }
    }

    public void RemoveTagsLinks( Recipe recipe )
    {
        recipe.Tags.Clear();
    }

    public async Task RemoveUnusedTags()
    {
        List<Tag> tags = await _tagRepository.GetAllTagsWithRecipeTags();

        List<Tag> tagsToRemove = tags.Where( tag => !tag.Recipes.Any() ).ToList();

        _tagRepository.RemoveTags( tagsToRemove );
    }
}
