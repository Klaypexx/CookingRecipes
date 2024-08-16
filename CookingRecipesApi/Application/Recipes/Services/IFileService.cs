using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Services;
public interface IFileService
{
    Task<string> CreateAvatar( IFormFile avatar, string rootPath );
    void RemoveAvatar( string avatar, string rootPath );
    Task<string> UpdateAvatar( IFormFile actualAvatar, string oldAvatar, string rootPath );
}
