namespace Application.Recipes.Services;
public static class ImageService
{
    public static FileStream CreateImage( string pathToFile, string rootPath )
    {
        string fullPath = Path.Combine( rootPath, "images", pathToFile );
        FileStream fileStream = new( fullPath, FileMode.Create );

        return fileStream;
    }

    public static void RemoveImage( string imagePath, string rootPath )
    {
        string fullPath = Path.Combine( rootPath, "images", imagePath );
        File.Delete( fullPath );
    }
}
