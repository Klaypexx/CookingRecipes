namespace Application.Auth.Utils;
public interface IPasswordHasher
{
    string GeneratePasswordHash( string password );
    bool VerifyPasswordHash( string password, string hashedPassword );
}
