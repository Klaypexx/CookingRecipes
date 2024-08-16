namespace Application.Recipes.Services;
public class ImageService : IImageService
{
    public FileStream CreateImage( string pathToFile, string rootPath )
    {
        string fullPath = Path.Combine( rootPath, "images", pathToFile );
        FileStream fileStream = new( fullPath, FileMode.Create );

        return fileStream;
    }

    public void RemoveImage( string imagePath, string rootPath )
    {
        string fullPath = Path.Combine( rootPath, "images", imagePath );
        File.Delete( fullPath );
    }
}
