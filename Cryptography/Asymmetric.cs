using System.Security.Cryptography;
using System.Text;

namespace DotnetCuritiba.Cryptography.Console.Cryptography;

public static class Asymmetric
{
    public static (string, string) GenerateRsa()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            string publicKey = rsa.ExportRSAPublicKeyPem();
            string privateKey = rsa.ExportRSAPrivateKeyPem();
            return (publicKey, privateKey);
        }
    }
    public static string Encrypt(string plainText, string publicKeyPem)
    {
        byte[] dataToEncrypt = Encoding.UTF8.GetBytes(plainText);

        using (var rsa = RSA.Create())
        {
            rsa.ImportFromPem(publicKeyPem);
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedData);
        }
    }

    public static string Decrypt(string encryptedText, string privateKeyPem)
    {
        byte[] encryptedData = Convert.FromBase64String(encryptedText);

        using (var rsa = RSA.Create())
        {
            rsa.ImportFromPem(privateKeyPem);
            byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }

}
