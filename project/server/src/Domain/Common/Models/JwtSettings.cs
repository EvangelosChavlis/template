namespace server.src.Domain.Common.Models;

public class JwtSettings
{
    public string PasswordKey { get; set; }
    public string TokenPublicKey { get; set; }
    public string TokenPrivateKey { get; set; }
    public string SensitiveDataPublicKey { get; set; }
    public string SensitiveDataPrivateKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}
