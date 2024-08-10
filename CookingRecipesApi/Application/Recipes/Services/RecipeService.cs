using Application.Recipes.Entities;
using Application.Recipes.Repositories;
using Application.Recipes.Utils;
using Application.Tags.Services;
using Domain.Recipes.Entities;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;
using TagApplication = Application.Recipes.Entities.Tag;
using TagDomain = Domain.Recipes.Entities.Tag;
using IngredientDomain = Domain.Recipes.Entities.Ingredient;
using StepDomain = Domain.Recipes.Entities.Step;
using Application.Foundation;
using Application.RecipesTags.Services;
using Application.Tags.Repositories;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IRecipeTagService _recipeTagService;
    public RecipeService( IRecipeRepository recipeRepository, ITagService tagService, IRecipeTagService recipeTagService, IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _recipeTagService = recipeTagService;
    }

    public async Task CreateRecipe( RecipeApplication recipe, string rootPath )
    {
        List<string>? tagsName = recipe.Tags?.Select( r => r.Name ).ToList();

        string avatarGuid = null;

        if ( recipe.Avatar != null )
        {
            avatarGuid = Guid.NewGuid().ToString() + Path.GetExtension( recipe.Avatar.FileName );
            using FileStream fileStream = ImageService.CreateImage( avatarGuid, rootPath );

            await recipe.Avatar.CopyToAsync( fileStream );
        }

        List<RecipeTag> tags = null;

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

    public async Task UpdateRecipe( RecipeApplication recipe, int recipeId, string rootPath )
    {
        RecipeDomain recipeDb = await GetByIdWithAllDetails( recipeId );

        List<string>? newTagsName = recipe.Tags?.Select( r => r.Name ).ToList();

        //Avatar
        string avatarGuid = null;

        if ( recipe.Avatar != null )
        {
            if ( recipeDb.Avatar != null )
            {
                ImageService.RemoveImage( recipeDb.Avatar, rootPath );
            }

            avatarGuid = Guid.NewGuid().ToString() + Path.GetExtension( recipe.Avatar.FileName );
            using FileStream fileStream = ImageService.CreateImage( avatarGuid, rootPath );

            await recipe.Avatar.CopyToAsync( fileStream );
        }

        //Tags

        //Удаление тегов
        List<int> tagsIdToDelete = recipeDb.Tags
        .Where( tag => newTagsName?.Contains( tag.Tag.Name ) != true )
        .Select( tag => tag.Tag.Id )
        .ToList();

        if ( tagsIdToDelete.Count != 0 )
        {
            await _recipeTagService.RemoveConnections( recipeDb.Id, tagsIdToDelete );

            await _tagService.RemoveTags( recipeId, tagsIdToDelete );


        }

        // Создание новых тегов/Установка связей
        if ( recipe.Tags != null )
        {
            List<TagDomain> existingTags = await _tagService.GetTagsByNames( newTagsName );

            if ( existingTags != null )
            {
                await _recipeTagService.CreateConnections( recipeId, existingTags.Select( tag => tag.Id ).ToList() );
            }

        }

        /*List<TagDomain>? newTags = newTagsName
        .Where( name => existingTags?.Any( t => t.Name.ToLower() == name.ToLower() ) != true )
        .Select( name => new TagDomain { Name = name.ToLower() } )
        .ToList();

        List<RecipeTag>? tags = newTags?.Select( t => new RecipeTag { Tag = t } ).ToList(); ;

        if ( newTags != null )
        {
            await _tagService.CreateTags( newTags ); // добавляем новые теги сразу со связбю к рецепту
        }*/

        //RecipeUpdate
        recipeDb.Name = recipe.Name;
        recipeDb.Description = recipe.Description;
        recipeDb.CookingTime = recipe.CookingTime;
        recipeDb.Portion = recipe.Portion;
        recipeDb.Avatar = recipe.Avatar != null ? avatarGuid : recipeDb.Avatar;
        recipeDb.Ingredients = recipe.Ingredients.Select( ingredientDto => new IngredientDomain
        {
            Name = ingredientDto.Name,
            Product = ingredientDto.Product
        } ).ToList();
        recipeDb.Steps = recipe.Steps.Select( stepDto => new StepDomain
        {
            Description = stepDto.Description,
        } ).ToList();
        /*recipeDb.Tags = tags;*/

        _recipeRepository.UpdateRecipe( recipeDb );
    }

    public async Task RemoveRecipe( int recipeId, string rootPath )
    {
        RecipeDomain recipe = await GetByIdWithTag( recipeId );

        if ( recipe.Tags != null )
        {
            await _tagService.RemoveTags( recipeId, recipe.Tags.Select( tag => tag.Tag.Id ).ToList() );
        }

        if ( recipe.Avatar != null )
        {
            ImageService.RemoveImage( recipe.Avatar, rootPath );
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
