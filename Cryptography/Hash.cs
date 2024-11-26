using System.Security.Cryptography;
using System.Text;

namespace DotnetCuritiba.Cryptography.Console.Cryptography;

public static class Hash
{
    public static string GenerateHash(string value)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha256.ComputeHash(valueBytes);

            return Convert.ToHexString(hashBytes);
        }
    }

    public static bool CompareHashes(string originalValue, string hashToCompare)
    {
        string originalHash = GenerateHash(originalValue);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(originalHash),
            Encoding.UTF8.GetBytes(hashToCompare));
    }
}
