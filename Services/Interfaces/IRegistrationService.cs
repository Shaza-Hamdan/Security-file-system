using InformationSecurity.DTO;

namespace InformationSecurity.Services
{
    public interface IRegistrationService
    {
        Task Register(CreateNewAccountRequest account);
        string Login(LoginRequest account);

    }
}