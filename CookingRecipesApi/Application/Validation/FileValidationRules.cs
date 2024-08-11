using Microsoft.AspNetCore.Http;

namespace Application.Validation;

public class FileValidationRules
{
    public bool IsImage( IFormFile file )
    {
        return file.ContentType == "image/jpeg" || file.ContentType == "image/png";
    }
}
