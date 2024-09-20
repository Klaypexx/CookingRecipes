namespace CookingRecipesApi.Extensions;

public static class CookieTokenExtension
{
    public static void SetRefreshTokenInsideCookie( this HttpContext context, string refreshToken, int lifetime )
    {
        CookieOptions cookieOption = new()
        {
            Expires = DateTimeOffset.Now.AddDays( lifetime ),
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        };

        context.Response.Cookies.Append( "refreshToken", refreshToken, cookieOption );
    }
}
