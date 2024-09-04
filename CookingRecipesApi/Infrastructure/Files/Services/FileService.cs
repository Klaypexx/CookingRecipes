﻿using Application.Files.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files.Services;

public class FileService : IFileService
{
    private readonly WebHostSetting _webHostSetting;
    private readonly FileSettings _fileSettings;

    public FileService( WebHostSetting webHostSetting, FileSettings fileSettings )
    {
        _webHostSetting = webHostSetting;
        _fileSettings = fileSettings;
    }

    public async Task<string> SaveImage( IFormFile image )
    {
        return await SaveFile( image, _fileSettings.ImagePathName );
    }

    public void RemoveImage( string imageName )
    {
        RemoveFile( imageName, _fileSettings.ImagePathName );
    }

    public async Task<string> UpdateImage( IFormFile actualImage, string oldPathToImage )
    {
        return await UpdateFile( actualImage, oldPathToImage, _fileSettings.ImagePathName );
    }

    /// <summary>
    /// Asynchronously saves an uploaded avatar file to a specified directory.
    /// Generates a unique file name for the saved file and returns the path to the file.
    /// </summary>
    /// <param name="avatar">The avatar file to be uploaded.</param>
    /// <returns>A string representing the path to the saved file, or null if no file was provided.</returns>
    private async Task<string> SaveFile( IFormFile file, string directoryName )
    {
        if ( file == null )
        {
            return null;
        }

        string pathToFile = Guid.NewGuid().ToString() + Path.GetExtension( file.FileName );

        string fullPath = Path.Combine( _webHostSetting.WebRootPath, directoryName, pathToFile );
        using FileStream fileStream = new( fullPath, FileMode.Create );

        await file.CopyToAsync( fileStream );

        return pathToFile;
    }

    private void RemoveFile( string fileName, string directoryName )
    {
        if ( fileName == null )
        {
            return;
        }

        string fullPath = Path.Combine( _webHostSetting.WebRootPath, directoryName, fileName );
        File.Delete( fullPath );
    }

    private async Task<string> UpdateFile( IFormFile actualfile, string oldPathToFile, string directoryName )
    {
        RemoveFile( oldPathToFile, directoryName );

        if ( actualfile == null )
        {
            return oldPathToFile;
        }

        string pathToFile = await SaveFile( actualfile, directoryName );

        return pathToFile;
    }
}
