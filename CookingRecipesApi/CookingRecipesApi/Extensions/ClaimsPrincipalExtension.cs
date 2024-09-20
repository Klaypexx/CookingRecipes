using System.Security.Claims;

namespace CookingRecipesApi.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetUserId( this ClaimsPrincipal user )
    {
        Claim claim = user.FindAll( x => x.Type == ClaimTypes.NameIdentifier ).FirstOrDefault();

        if ( claim?.Value is null )
        {
            throw new ArgumentException( "Id пользователя не найден" );
        }

        return claim.Value;
    }

    public static string GetNameOfUser( this ClaimsPrincipal user )
    {
        Claim claim = user.FindAll( x => x.Type == ClaimTypes.Name ).FirstOrDefault();

        if ( claim?.Value is null )
        {
            throw new ArgumentException( "Имя пользователя не найдено" );
        }

        return claim.Value;
    }

    public static string GetUserName( this ClaimsPrincipal user )
    {
        Claim claim = user.FindAll( x => x.Type == "username" ).FirstOrDefault();

        if ( claim?.Value is null )
        {
            throw new ArgumentException( "Username пользователя не найден" );
        }

        return claim.Value;
    }
}
