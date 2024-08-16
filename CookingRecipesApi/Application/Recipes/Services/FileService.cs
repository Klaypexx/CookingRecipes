using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Services;
public class FileService : IFileService
{
    private readonly IImageService _imageService;

    public FileService( IImageService imageService )
    {
        _imageService = imageService;
    }

    public async Task<string> SaveFile( IFormFile avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return null;
        }

        string pathToFile = Guid.NewGuid().ToString() + Path.GetExtension( avatar.FileName );
        using FileStream fileStream = _imageService.CreateImage( pathToFile, rootPath );

        await avatar.CopyToAsync( fileStream );

        return pathToFile;
    }

    public void RemoveFile( string avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return;
        }

        _imageService.RemoveImage( avatar, rootPath );
    }

    public async Task<string> UpdateFile( IFormFile actualAvatar, string oldAvatar, string rootPath )
    {
        if ( actualAvatar == null )
        {
            return oldAvatar;
        }

        RemoveFile( oldAvatar, rootPath );

        string pathToFile = await SaveFile( actualAvatar, rootPath );

        return pathToFile;
    }
}
