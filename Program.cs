using DotnetCuritiba.Cryptography.Console.Cryptography;


Console.WriteLine("=== DOTNET CURITIBA 2025 ===");

Console.WriteLine("\n--- Hash ---");
if (ConfirmAction("Deseja gerar e comparar um hash?"))
{
    Console.Clear();
    Console.Write("Digite o texto para gerar o hash: ");
    string valueToHash = Console.ReadLine() ?? string.Empty;
    string hashValue = Hash.GenerateHash(valueToHash);
    Console.WriteLine($"Hash gerado: {hashValue}");

    Console.Write("Digite o texto para comparar com o hash: ");
    string valueToCompare = Console.ReadLine() ?? string.Empty;
    bool isSameHash = Hash.CompareHashes(valueToCompare, hashValue);
    Console.WriteLine($"Os valores são iguais? {isSameHash}");
}

Console.WriteLine("\n--- AES ---");
if (ConfirmAction("Deseja criptografar e descriptografar um texto com AES?"))
{
    Console.Clear();
    string aesKey = Symmetric.GenerateAesKey();
    Console.WriteLine($"Chave AES gerada: {aesKey}");

    Console.Write("Digite o texto para criptografar com AES: ");
    string plainText = Console.ReadLine() ?? string.Empty;
    string encryptedText = Symmetric.Encrypt(plainText, aesKey);
    Console.WriteLine($"Texto criptografado (AES): {encryptedText}");

    string decryptedText = Symmetric.Decrypt(encryptedText, aesKey);
    Console.WriteLine($"Texto descriptografado (AES): {decryptedText}");
}

Console.WriteLine("\n--- RSA ---");
if (ConfirmAction("Deseja criptografar e descriptografar um texto com RSA?"))
{
    Console.Clear();
    var (publicKeyPem, privateKeyPem) = Asymmetric.GenerateRsa();
    Console.WriteLine($"Chave pública gerada:\n{publicKeyPem}");
    Console.WriteLine($"Chave privada gerada: ***");

    Console.Write("Digite o texto para criptografar com RSA: ");
    string rsaPlainText = Console.ReadLine() ?? string.Empty;
    string rsaEncryptedText = Asymmetric.Encrypt(rsaPlainText, publicKeyPem);
    Console.WriteLine($"Texto criptografado (RSA): {rsaEncryptedText}");

    string rsaDecryptedText = Asymmetric.Decrypt(rsaEncryptedText, privateKeyPem);
    Console.WriteLine($"Texto descriptografado (RSA): {rsaDecryptedText}");
}

Console.WriteLine("\nProcesso concluído!");
static bool ConfirmAction(string message)
{
    Console.WriteLine(message + " (s/n)");
    var input = Console.ReadLine()?.ToLower();
    return input == "s" || input == "sim";
}
