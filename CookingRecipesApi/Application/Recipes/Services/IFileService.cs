using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Services;
public interface IFileService
{
    Task<string> SaveFile( IFormFile avatar, string rootPath );
    void RemoveFile( string avatar, string rootPath );
    Task<string> UpdateFile( IFormFile actualAvatar, string oldAvatar, string rootPath );
}
