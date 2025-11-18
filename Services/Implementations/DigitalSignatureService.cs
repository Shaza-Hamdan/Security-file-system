using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using InformationSecurity.DTO;
using Microsoft.AspNetCore.Http;

namespace InformationSecurity.Services.Implementations
{
    public class DigitalSignatureService : IDigitalSignatureService
    {
        // Generate RSA-16384 Key Pair (Private and Public keys)

        public RSA GenerateRSAKeyPair()
        {
            // RSA with key size of 16384 bits
            RSA rsa = RSA.Create(3072);
            return rsa;
        }

        // Sign file with the RSA private key
        public SignResponseDto SignData(RSA rsa, IFormFile file)
        {
            if (rsa == null || file == null)
                throw new ArgumentException("Invalid RSA key or file to sign.");

            // Read the file's bytes
            using (var memoryStream = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                // Hash the file data
                using (var sha256 = SHA256.Create())
                {
                    byte[] fileHash = sha256.ComputeHash(fileBytes);

                    // Sign the file hash with the RSA private key
                    byte[] signature = rsa.SignHash(fileHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    string publicKeyXml = rsa.ToXmlString(false);
                    return new SignResponseDto
                    (
                        Signature: Convert.ToBase64String(signature),
                        rsa: publicKeyXml
                    );
                }
            }
        }

        // Verify the signature with the RSA public key
        public bool VerifySignature(RSA rsa, IFormFile file, string signature)
        {
            if (rsa == null || file == null || string.IsNullOrEmpty(signature))
                throw new ArgumentException("Invalid RSA key, file, or signature.");

            // Read the file's bytes
            using (var memoryStream = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                // Hash the file data
                using (var sha256 = SHA256.Create())
                {
                    byte[] fileHash = sha256.ComputeHash(fileBytes);

                    // Convert Base64 signature to byte array
                    byte[] signatureBytes = Convert.FromBase64String(signature);

                    // Verify the signature with the RSA public key
                    return rsa.VerifyHash(fileHash, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
        }
        public RSA LoadPublicKey(string publicKey)
        {
            RSA rsa = RSA.Create();
            rsa.FromXmlString(publicKey);  // This will parse the XML and load the public key
            return rsa;


        }
    }
}
