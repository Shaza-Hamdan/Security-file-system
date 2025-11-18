using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using InformationSecurity.Services;

public class AES_EncDecService : IAES_EncDecService
{
    private readonly string _key; // AES encryption key (128, 192, or 256 bits)
    private readonly string _iv;  // Initialization Vector (128 bits)

    public AES_EncDecService(string key, string iv)
    {
        _key = key ?? throw new ArgumentNullException(nameof(key));
        _iv = iv ?? throw new ArgumentNullException(nameof(iv));
    }
    public async Task<string> EncryptFileAsync(string filePath)
    {
        string uniqueId = Guid.NewGuid().ToString();

        var encryptedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "EncryptedFiles", $"{uniqueId}.enc");

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);
            aes.Padding = PaddingMode.PKCS7;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var encryptedStream = new FileStream(encryptedFilePath, FileMode.Create, FileAccess.Write))
            using (var cryptoStream = new CryptoStream(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                await fileStream.CopyToAsync(cryptoStream);
            }
        }
        return uniqueId;
    }


    public async Task<string> DecryptFileAsync(string uniqueId)
    {
        Console.WriteLine($"D_AES Key: {_key}");
        Console.WriteLine($"D_AES IV: {_iv}");

        // Locate the encrypted file by its unique ID
        string encryptedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "EncryptedFiles", $"{uniqueId}.enc");
        string decryptedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DecryptedFiles", $"{uniqueId}_decrypted.pdf"); // Adjust file extension

        if (!File.Exists(encryptedFilePath))
            throw new FileNotFoundException("Encrypted file not found.");

        // Display the Encrypted file size
        FileInfo encryptedFileInfo = new FileInfo(encryptedFilePath);
        Console.WriteLine($"Encrypted file size: {encryptedFileInfo.Length} bytes");

        Directory.CreateDirectory(Path.GetDirectoryName(decryptedFilePath)); // Ensure output directory exists

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);
            aes.Padding = PaddingMode.PKCS7;

            try
            {
                using (var encryptedStream = new FileStream(encryptedFilePath, FileMode.Open, FileAccess.Read))
                using (var decryptedStream = new FileStream(decryptedFilePath, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(encryptedStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    await cryptoStream.CopyToAsync(decryptedStream);
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Decryption failed due to invalid padding or corrupted data.");
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Display the decrypted file size
        FileInfo decryptedFileInfo = new FileInfo(decryptedFilePath);
        Console.WriteLine($"Decrypted file size: {decryptedFileInfo.Length} bytes");

        return decryptedFilePath;
    }

}
