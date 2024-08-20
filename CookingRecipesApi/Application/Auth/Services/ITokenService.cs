using Domain.Auth.Entities;

namespace Application.Auth.Services;

public interface ITokenService
{
    string GenerateJwtToken( User user );
    string GenerateRefreshToken();
}
