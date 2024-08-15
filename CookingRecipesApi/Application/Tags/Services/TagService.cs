using Application.Foundation;
using Application.Tags.Repositories;
using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TagService( ITagRepository tagRepository, IUnitOfWork unitOfWork )
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ActualizeTags( Recipe recipe )
    {
        List<string> tagsName = recipe.Tags.Select( tag => tag.Tag.Name.ToLower() ).ToList(); // actualTagsName

        if ( tagsName.Count != 0 )
        {
            List<Tag> existingTags = await _tagRepository.GetTagsByNames( tagsName );

            List<Tag> tagsToCreate = tagsName
                   .Where( name => !existingTags.Any( t => t.Name.ToLower() == name ) )
                   .Select( name => new Tag { Name = name.ToLower() } )
                   .ToList();

            await _tagRepository.CreateTags( tagsToCreate );
        }

    }

    public async Task CreatingLinksWithTags( Recipe recipe ) // передавать actualRecipeTags
    {
        List<string> tagsName = recipe.Tags.Select( tag => tag.Tag.Name.ToLower() ).ToList(); //actual

        if ( tagsName.Count != 0 )
        {
            List<Tag> existingTags = await _tagRepository.GetTagsByNames( tagsName );

            List<RecipeTag> recipeTags = existingTags.Select( tag => new RecipeTag
            {
                TagId = tag.Id,
                Tag = tag
            } ).ToList();

            recipe.Tags = recipeTags;
        }
    }

    public async Task RemoveTagsLinks( Recipe recipe )
    {
        recipe.Tags.Clear();

        await _unitOfWork.Save();
    }

    public async Task RemoveUnusedTags()
    {
        List<Tag> tags = await _tagRepository.GetAllTagsWithRecipeTags();

        List<Tag> tagsToRemove = tags.Where( tag => !tag.Recipes.Any() ).ToList();

        _tagRepository.RemoveTags( tagsToRemove );
    }
}
