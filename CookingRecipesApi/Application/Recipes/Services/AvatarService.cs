using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Services;
public static class AvatarService
{
    public async static Task<string> CreateAvatar( RecipeApplication recipe, string rootPath )
    {
        if ( recipe.Avatar == null ) return null;

        string avatarGuid = Guid.NewGuid().ToString() + Path.GetExtension( recipe.Avatar.FileName );
        using FileStream fileStream = ImageService.CreateImage( avatarGuid, rootPath );

        await recipe.Avatar.CopyToAsync( fileStream );

        return avatarGuid;
    }

    public static void RemoveAvatar( RecipeDomain recipe, string rootPath )
    {
        if ( recipe.Avatar == null ) return;

        ImageService.RemoveImage( recipe.Avatar, rootPath );
    }

    public async static Task<string> UpdateAvatar( RecipeApplication newRecipe, RecipeDomain oldRecipe, string rootPath )
    {
        if ( newRecipe.Avatar == null ) return oldRecipe.Avatar;

        RemoveAvatar( oldRecipe, rootPath );

        string avatarGuid = await CreateAvatar( newRecipe, rootPath );

        return avatarGuid;
    }
}
