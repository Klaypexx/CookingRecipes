using Application.Auth.Services;
using Domain.Auth;
using Domain.Auth.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CookingRecipesApi.Auth;

public class TokenService : ITokenService
{
    private readonly AuthSettings _authSettings;

    public TokenService( AuthSettings authSettings )
    {
        _authSettings = authSettings;
    }

    public string GenerateJwtToken( User user )
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        ClaimsIdentity identity = new( claims, "Token" );

        DateTime now = DateTime.UtcNow;

        JwtSecurityToken jwt = new(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add( TimeSpan.FromMinutes( _authSettings.LifeTime ) ),
                signingCredentials: new SigningCredentials( GetSymmetricSecurityKey( _authSettings.Key ), SecurityAlgorithms.HmacSha256 ) );

        return new JwtSecurityTokenHandler().WriteToken( jwt );
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[ 32 ];
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes( randomNumber );
        return Convert.ToBase64String( randomNumber );
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey( string key )
    {
        return new SymmetricSecurityKey( Encoding.ASCII.GetBytes( key ) );
    }

    public void SetRefreshTokenInsideCookie( string refreshToken, HttpContext context )
    {
        context.Response.Cookies.Append( "refreshToken", refreshToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays( _authSettings.RefreshLifeTime ),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            } );
    }
}
