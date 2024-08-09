using CookingRecipesApi.Auth;

namespace CookingRecipesApi.Utilities;
public static class CookieToken
{
    public static void SetRefreshTokenInsideCookie( this HttpContext context, string refreshToken, int lifetime )
    {
        context.Response.Cookies.Append( "refreshToken", refreshToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays( lifetime ),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            } );
    }
}
