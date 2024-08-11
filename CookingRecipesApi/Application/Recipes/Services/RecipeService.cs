using Application.Recipes.Repositories;
using Application.Recipes.Utils;
using Application.Tags.Services;
using Domain.Recipes.Entities;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;
using TagDomain = Domain.Recipes.Entities.Tag;
using Application.RecipesTags.Services;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IRecipeTagService _recipeTagService;
    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IRecipeTagService recipeTagService )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _recipeTagService = recipeTagService;
    }

    public async Task CreateRecipe( RecipeApplication recipe, string rootPath )
    {
        List<string>? tagsName = recipe.Tags?.Select( r => r.Name ).ToList();

        string? avatarGuid = await AvatarService.CreateAvatar( recipe, rootPath );

        List<RecipeTag>? tags = new List<RecipeTag>();

        if ( recipe.Tags != null )
        {
            List<TagDomain> existingTags = await _tagService.GetTagsByNames( tagsName );

            List<TagDomain> newTags = tagsName
            .Where( name => !existingTags.Any( t => t.Name.ToLower() == name.ToLower() ) )
            .Select( name => new TagDomain { Name = name.ToLower() } )
            .ToList();

            await _tagService.CreateTags( newTags );

            tags = existingTags
            .Select( t => new RecipeTag { Tag = t } )
            .Concat( newTags.Select( t => new RecipeTag { Tag = t } ) )
            .ToList();
        }

        await _recipeRepository.CreateRecipe( recipe.Create( tags, avatarGuid ) );
    }

    public async Task UpdateRecipe( RecipeApplication newRecipe, int recipeId, string rootPath )
    {
        RecipeDomain oldRecipe = await GetByIdWithAllDetails( recipeId );

        List<string>? newTagsName = newRecipe.Tags?.Select( r => r.Name ).ToList();
        List<string>? oldTagName = oldRecipe.Tags?.Select( r => r.Tag.Name ).ToList();

        //Avatar
        string? avatarGuid = await AvatarService.UpdateAvatar( newRecipe, oldRecipe, rootPath );

        //Tags

        //Удаление тегов
        List<int>? tagsIdToDelete = oldRecipe.Tags?
            .Where( tag => newTagsName?.Contains( tag.Tag.Name ) != true )
            .Select( tag => tag.Tag.Id )
            .ToList();

        if ( tagsIdToDelete?.Count != 0 )
        {
            await _recipeTagService.RemoveConnections( oldRecipe.Id, tagsIdToDelete );

            await _tagService.RemoveTags( recipeId, tagsIdToDelete );
        }

        // Создание новых тегов/Установка связей
        List<RecipeTag>? tags = new List<RecipeTag>();

        if ( newRecipe.Tags != null )
        {

            // Добавление тегов которые уже есть в базе данных, при этом которые еще не имеют свящей с нашим рецептом
            List<TagDomain> existingTags = await _tagService.GetTagsByNames( newTagsName );

            List<TagDomain>? existingTagsWithoutOldNames = existingTags?.Where( t => oldTagName?.Contains( t.Name ) != true ).ToList();

            // Добавление новых тегов, которых еще нет в базе данных
            List<TagDomain> newTags = newTagsName
                .Where( name => existingTags.Any( t => t.Name.ToLower() == name.ToLower() ) != true )
                .Select( name => new TagDomain { Name = name.ToLower() } )
                .ToList();

            await _tagService.CreateTags( newTags );

            tags = existingTagsWithoutOldNames?
               .Select( t => new RecipeTag { Tag = t } )
               .Concat( newTags.Select( t => new RecipeTag { Tag = t } ) )
               .ToList();
        }

        //RecipeUpdate
        oldRecipe.UpdateRecipe( newRecipe.Create( tags, avatarGuid ) );

        _recipeRepository.UpdateRecipe( oldRecipe );
    }

    public async Task RemoveRecipe( int recipeId, string rootPath )
    {
        RecipeDomain recipe = await GetByIdWithTag( recipeId );

        if ( recipe.Tags != null )
        {
            await _tagService.RemoveTags( recipeId, recipe.Tags.Select( tag => tag.Tag.Id ).ToList() );
        }

        AvatarService.RemoveAvatar( recipe, rootPath );

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
