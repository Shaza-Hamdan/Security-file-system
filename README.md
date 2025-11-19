A backend application built with .NET, Entity Framework, and MySQL that provides secure file handling, strong authentication, and modern cryptographic features.
The system allows users to register, log in, upload files, encrypt them with AES, sign them using RSA, and verify signatures.
Features:
🔐 Authentication & Authorization:
1.User Login using secure hash verification (GOST R 34.11-2012 (Stribog) hashing algorithm and salt).
2.Role-based access control (Guest/ User / Admin).
3.JWT token authentication for protected endpoints.

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

🔑 Default Admin User:
When the application starts for the first time, it automatically creates an initial admin account in the database:
Email: admin@example.com
Password: adminpassword
This user has full administrator privileges and can be used to log in immediately after starting the API.

▶️ How to Run the Project:
Make sure you have the .NET SDK installed.
Then open the project in VS Code and run: 
1. dotnet build
2. dotnet run
Once the backend starts, you can open the API documentation and test all endpoints using Swagger at:
http://localhost:5000/swagger  (or the port shown in your terminal)
