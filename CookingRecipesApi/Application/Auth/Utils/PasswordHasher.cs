namespace Infrastructure.Auth.Utils;
public static class PasswordHasher
{
    public static string GeneratePasswordHash( string password ) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword( password );

    public static bool VerifyPasswordHash( string password, string hashedPassword ) =>
        BCrypt.Net.BCrypt.EnhancedVerify( password, hashedPassword );
}
