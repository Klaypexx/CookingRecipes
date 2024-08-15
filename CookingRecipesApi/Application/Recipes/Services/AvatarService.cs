using Microsoft.AspNetCore.Http;
using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Services;
public static class AvatarService
{
    public async static Task<string> CreateAvatar( IFormFile avatar, string rootPath ) // сразу передавать recipe.Avatar
    {
        if ( avatar == null )
        {
            return null;
        }

        string avatarGuid = Guid.NewGuid().ToString() + Path.GetExtension( avatar.FileName );
        using FileStream fileStream = ImageService.CreateImage( avatarGuid, rootPath );

        await avatar.CopyToAsync( fileStream );

        return avatarGuid;
    }

    public static void RemoveAvatar( string avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return;
        }

        ImageService.RemoveImage( avatar, rootPath );
    }

    public async static Task<string> UpdateAvatar( IFormFile actualAvatar, string oldAvatar, string rootPath )
    {
        if ( actualAvatar == null )
        {
            return oldAvatar;
        }

        RemoveAvatar( oldAvatar, rootPath );

        string avatarGuid = await CreateAvatar( actualAvatar, rootPath );

        return avatarGuid;
    }
}
