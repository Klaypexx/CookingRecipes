using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Services;
public static class AvatarService
{
    public async static Task<string> CreateAvatar( IFormFile avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return null;
        }

        string pathToFile = Guid.NewGuid().ToString() + Path.GetExtension( avatar.FileName );
        using FileStream fileStream = ImageService.CreateImage( pathToFile, rootPath );

        await avatar.CopyToAsync( fileStream );

        return pathToFile;
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

        string pathToFile = await CreateAvatar( actualAvatar, rootPath );

        return pathToFile;
    }
}
