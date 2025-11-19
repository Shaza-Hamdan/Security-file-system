A backend application built with .NET, Entity Framework, and MySQL that provides secure file handling, strong authentication, and modern cryptographic features.
The system allows users to register, log in, upload files, encrypt them with AES, sign them using RSA, and verify signatures.
Features:
🔐 Authentication & Authorization:
*User Login using secure hash verification (GOST R 34.11-2012 (Stribog) hashing algorithm and salt)
*Role-based access control (Guest/ User / Admin)
*JWT token authentication for protected endpoints

🔏 AES File Encryption:
*Encrypts uploaded files (pdfs) using AES-256-CBC
*Secure IV handling
*Safe file storage with .enc extension
*Allows users to download decrypted content securely

✍️ Digital Signature System (RSA):
*Generates RSA key pairs
*Signs file data using the private key
*Uses SHA-256 hashing before signing
*Verification is done using the public key

🧪 Technologies Used:
*C# / .NET 7
*ASP.NET Core Web API
*Entity Framework Core
*MySQL Database
*AES Encryption (System.Security.Cryptography)
*RSA Digital Signatures
*SHA-256 Hashing
*Stribog Hash + Salt
*JWT Authentication

📁 Project Structure:
/Controllers       → API endpoints  
/Services          → AES, RSA, Digital Signature, Auth service  
/DTO               → Data transfer objects  
/Entity            → Database entities  
/Interfaces        → Service interfaces  

