using InformationSecurity.DTO;
using System.Security.Cryptography;

namespace InformationSecurity.Services
{
    public interface IAES_EncDecService
    {
        Task<string> EncryptFileAsync(string filePath);  // Encrypt a file
        Task<string> DecryptFileAsync(string uniqueId);  // Decrypt a file by unique ID
    }
}