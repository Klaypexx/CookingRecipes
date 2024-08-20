using Microsoft.AspNetCore.Http;

namespace Application.Files.Services;

public interface IFileService
{
    Task<string> SaveImage( IFormFile image );
    void RemoveImage( string imageName );
    Task<string> UpdateImage( IFormFile actualImage, string oldPathToImage );
}
