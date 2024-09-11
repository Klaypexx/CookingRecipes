using Application.Auth;

namespace Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string GeneratePasswordHash( string password ) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword( password );

    public bool VerifyPasswordHash( string password, string hashedPassword ) =>
        BCrypt.Net.BCrypt.EnhancedVerify( password, hashedPassword );
}
