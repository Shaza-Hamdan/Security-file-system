using System;
using System.Security.Cryptography;
using System.IO;

namespace DigitalSignature
{
    public class DigitalSignatureService
    {
        public static RSA CreateRsaKeyPair()
        {
            RSA rsa = RSA.Create(16384);  // 16384-bit key size
            return rsa;
        }

        public static byte[] SignData(RSA rsa, string data)
        {
            // Convert the data to a byte array
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);

            // Create a hash of the data (SHA-256 or SHA-512)
            using (SHA256 sha256 = SHA256.Create())  // You can use SHA512.Create() for stronger hashing
            {
                byte[] hash = sha256.ComputeHash(dataBytes);

                // Sign the hash using RSA
                byte[] signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return signature;
            }
        }

        public static bool VerifySignature(RSA rsa, string data, byte[] signature)
        {
            // Convert the data to a byte array
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);

            // Create a hash of the data (use the same hashing algorithm as during signing)
            using (SHA256 sha256 = SHA256.Create())  // Use the same hash algorithm
            {
                byte[] hash = sha256.ComputeHash(dataBytes);

                // Verify the signature using the public key (RSA)
                return rsa.VerifyHash(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
    }
}