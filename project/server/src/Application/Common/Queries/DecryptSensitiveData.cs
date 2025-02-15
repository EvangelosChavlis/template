// packages
using System.Security.Cryptography;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Common.Models;

namespace server.src.Application.Common.Queries;

public record DecryptSensitiveDataQuery(string EncryptedData) : IRequest<object>;

public class DecryptSensitiveDataHandler : IRequestHandler<DecryptSensitiveDataQuery, object>
{
    private readonly JwtSettings _jwtSettings;
    private readonly RSA _sensitiveDataPrivateKey;

    public DecryptSensitiveDataHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;

        _sensitiveDataPrivateKey = RSA.Create();
        _sensitiveDataPrivateKey.ImportRSAPrivateKey(Convert.FromBase64String(_jwtSettings.SensitiveDataPrivateKey), out _);
    }

    // Implement generic Handle method for DecryptSensitiveDataQuery<T>
    public Task<object> Handle(DecryptSensitiveDataQuery query, CancellationToken token = default)
    {
        // Decrypt the data as before
        var encryptedBytes = Convert.FromBase64String(query.EncryptedData);

        int keySize = _sensitiveDataPrivateKey.KeySize / 8;
        int ivSize = keySize;

        var encryptedAesKey = encryptedBytes[..keySize];
        var encryptedAesIV = encryptedBytes[keySize..(keySize + ivSize)];
        var actualEncryptedData = encryptedBytes[(keySize + ivSize)..];

        var aesKey = _sensitiveDataPrivateKey.Decrypt(encryptedAesKey, RSAEncryptionPadding.OaepSHA256);
        var aesIV = _sensitiveDataPrivateKey.Decrypt(encryptedAesIV, RSAEncryptionPadding.OaepSHA256);

        var aes = Aes.Create();
        var decryptor = aes.CreateDecryptor(aesKey, aesIV);
        var decryptedBytes = decryptor.TransformFinalBlock(actualEncryptedData, 0, actualEncryptedData.Length);

        var decryptedData = Encoding.UTF8.GetString(decryptedBytes);

        var deserializedData = System.Text.Json.JsonSerializer.Deserialize<object>(decryptedData);

        return Task.FromResult(deserializedData)!;
    }
}
