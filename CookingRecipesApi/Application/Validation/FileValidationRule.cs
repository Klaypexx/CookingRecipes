using Microsoft.AspNetCore.Http;

namespace Application.Validation;

public class FileValidationRule
{
    public bool IsImage( IFormFile file )
    {
        return file.ContentType == "image/jpeg" || file.ContentType == "image/png";
    }
}
