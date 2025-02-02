namespace server.src.Domain.Models.Auth;

public class JwtSettings
{
    public string PasswordKey { get; set; }
    public string SensitiveDataKey { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
