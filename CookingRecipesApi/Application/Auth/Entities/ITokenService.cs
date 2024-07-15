using Domain.Auth.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Auth.Entities;
public interface ITokenService
{
    string GenerateJwtToken( User user );
    string GenerateRefreshToken();
    void SetRefreshTokenInsideCookie( string refreshToken, HttpContext context );
}
