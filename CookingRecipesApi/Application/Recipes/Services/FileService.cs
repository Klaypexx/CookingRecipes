using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Services;

public class FileService : IFileService
{
    private readonly string _filePathName = "images";
    public async Task<string> SaveFile( IFormFile avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return null;
        }

        string pathToFile = Guid.NewGuid().ToString() + Path.GetExtension( avatar.FileName );

        string fullPath = Path.Combine( rootPath, _filePathName, pathToFile );
        using FileStream fileStream = new( fullPath, FileMode.Create );

        await avatar.CopyToAsync( fileStream );

        return pathToFile;
    }

    public void RemoveFile( string avatar, string rootPath )
    {
        if ( avatar == null )
        {
            return;
        }

        string fullPath = Path.Combine( rootPath, _filePathName, avatar );
        File.Delete( fullPath );
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
