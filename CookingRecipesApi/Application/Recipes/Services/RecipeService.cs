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

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    public RecipeService( IRecipeRepository recipeRepository, ITagService tagService, IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
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
        List<string>? oldTagsName = recipeDb.Tags?.Select( r => r.Tag.Name ).ToList();

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

        if ( recipe.Tags != null )
        {
            //Удаление тегов
            List<string> tagsNameToDelete = recipeDb.Tags
            .Where( tag => !newTagsName.Contains( tag.Tag.Name ) )
            .Select( tag => tag.Tag.Name )
            .ToList();

            await _tagService.RemoveTags( recipeId, tagsNameToDelete );


            //Добавление новых тегов
            List<TagDomain> existingTags = await _tagService.GetTagsByNames( newTagsName );

            List<TagDomain> newTags = newTagsName
                 .Where( name => !existingTags.Any( t => t.Name.ToLower() == name.ToLower() ) )
                 // тут должна создаться связь рецепта и тега за счет добавления id рецепта, но запись жутко выглядит, надо рефакторить
                 .Select( name => new TagDomain { Name = name.ToLower(), Recipes = new() { new() { RecipeId = recipeId } } } )
                 .ToList();

            await _tagService.CreateTags( newTags );
        }
        else
        {
            await _tagService.RemoveTags( recipeId, oldTagsName );
        }

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

        _recipeRepository.UpdateRecipe( recipeDb );
    }

    public async Task RemoveRecipe( int recipeId, string rootPath )
    {
        RecipeDomain recipe = await GetByIdWithTag( recipeId );

        if ( recipe.Tags != null )
        {
            await _tagService.RemoveTags( recipeId, recipe.Tags.Select( tag => tag.Tag.Name ).ToList() );
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
