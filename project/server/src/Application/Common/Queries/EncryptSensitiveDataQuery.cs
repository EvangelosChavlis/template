// packages
using System.Security.Cryptography;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Common.Models;

namespace server.src.Application.Common.Queries;

public record EncryptSensitiveDataQuery(object Data) : IRequest<string>;

public class EncryptSensitiveDataHandler : IRequestHandler<EncryptSensitiveDataQuery, string>
{
    private readonly JwtSettings _jwtSettings;
    private readonly RSA _sensitiveDataPublicKey;
    

    public EncryptSensitiveDataHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;

        _sensitiveDataPublicKey = RSA.Create();
        _sensitiveDataPublicKey.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_jwtSettings.SensitiveDataPublicKey), out _);
    }


    public Task<string> Handle(EncryptSensitiveDataQuery query, CancellationToken token = default)
    {
        // Serialize the data to byte array before encryption
        if (query.Data is null) 
            throw new ArgumentNullException(nameof(query.Data));
        
        var dataBytes =  Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(query.Data));

        var aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var encryptedData = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

        var encryptedAesKey = _sensitiveDataPublicKey.Encrypt(aes.Key, RSAEncryptionPadding.OaepSHA256);
        var encryptedAesIV = _sensitiveDataPublicKey.Encrypt(aes.IV, RSAEncryptionPadding.OaepSHA256);

        var encryptedPackage = new List<byte>();
        encryptedPackage.AddRange(encryptedAesKey);
        encryptedPackage.AddRange(encryptedAesIV);
        encryptedPackage.AddRange(encryptedData);

        var encryptedSensitiveData = Convert.ToBase64String(encryptedPackage.ToArray());

        return Task.FromResult(encryptedSensitiveData);
    }

}