namespace Application.Recipes.Services;
public interface IImageService
{
    FileStream CreateImage( string pathToFile, string rootPath );
    void RemoveImage( string imagePath, string rootPath );
}
