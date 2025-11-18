using System.Data;
using InformationSecurity.DTO;
using InformationSecurity.Persistence.entity;
using InformationSecurity.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using ResFunctions;


namespace InformationSecurity.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDBContext appdbContext;
        private readonly RegistrationFunctions registrationFunctions;
        public RegistrationService(AppDBContext appDbContext, RegistrationFunctions registrationfunctions)
        {
            appdbContext = appDbContext;
            registrationFunctions = registrationfunctions;
        }

        public async Task Register(CreateNewAccountRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.PhoneNumber))
            {
                throw new ArgumentException("All fields must be provided.");
            }
            //create a unique salt
            string salt = registrationFunctions.GenerateSalt();
            if (string.IsNullOrEmpty(salt))
            {
                throw new Exception("Salt generation failed.");
            }
            // Hashing a password with salt using Stribog
            string passwordHash = registrationFunctions.HashPasswordWithStribog(request.Password, salt);

            var role = await appdbContext.Roles.SingleOrDefaultAsync(r => r.Name == "Guest");
            if (role == null)
            {
                throw new Exception("Role 'User' not found.");
            }

            // Create the user entity
            var user = new Registration
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                roleId = role.Id,
                PhoneNumber = request.PhoneNumber
            };

            appdbContext.registrations.Add(user);
            await appdbContext.SaveChangesAsync();
        }


        public string Login(LoginRequest account)
        {
            var user = appdbContext.registrations.Include(u => u.role)
                                          .SingleOrDefault(u => u.Email == account.Email);

            if (user == null)
            {
                return "NotFound";
            }

            string hashedInputPassword = registrationFunctions.HashPasswordWithStribog(account.Password, user.Salt);

            if (hashedInputPassword != user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = registrationFunctions.GenerateJwtToken(user);
            return token;

        }
    }
}