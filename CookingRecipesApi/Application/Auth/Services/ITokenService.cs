using Domain.Auth.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Auth.Services;
public interface ITokenService
{
    string GenerateJwtToken( User user );
    string GenerateRefreshToken();
}
