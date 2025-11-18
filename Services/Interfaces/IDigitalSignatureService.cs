using InformationSecurity.DTO;
using System.Security.Cryptography;

namespace InformationSecurity.Services
{
    public interface IDigitalSignatureService
    {
        RSA GenerateRSAKeyPair();

        // Method to sign the data (file) with the RSA private key
        SignResponseDto SignData(RSA rsa, IFormFile file);
        RSA LoadPublicKey(string publicKey);
        // Method to verify the signature using the RSA public key
        bool VerifySignature(RSA rsa, IFormFile file, string signature);
    }

}