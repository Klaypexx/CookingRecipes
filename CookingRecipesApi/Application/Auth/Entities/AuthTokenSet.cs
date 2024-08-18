namespace Application.Auth.Entities;

public class AuthTokenSet
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}
