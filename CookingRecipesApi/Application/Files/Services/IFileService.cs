using Microsoft.AspNetCore.Http;

namespace Application.Files.Services;

public interface IFileService
{
    Task<string> SaveImage( IFormFile image, string directoryName );
    void RemoveImage( string imageName, string directoryName );
    Task<string> UpdateImage( IFormFile actualImage, string oldPathToImage, string directoryName );
}
