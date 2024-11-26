using System.Security.Cryptography;
using System.Text;

namespace DotnetCuritiba.Cryptography.Console.Cryptography;

public static class Symmetric
{
    public static string GenerateAesKey()
    {
        using var aes = Aes.Create();
        aes.GenerateKey();
        return Convert.ToBase64String(aes.Key);
    }
    public static string Encrypt(string plainText, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);

        aes.GenerateIV();
        var iv = aes.IV;

        using var encryptor = aes.CreateEncryptor();
        var dataBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

        var result = $"{Convert.ToBase64String(iv)}:{Convert.ToBase64String(encryptedBytes)}";
        return result;
    }

    public static string Decrypt(string encryptedData, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);

        var parts = encryptedData.Split(':');

        if (parts.Length != 2)
            throw new Exception("Invalid encrypted data format");

        var iv = Convert.FromBase64String(parts[0]);
        var encryptedBytes = Convert.FromBase64String(parts[1]);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}

