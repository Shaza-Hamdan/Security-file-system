using InformationSecurity.DTO;
using InformationSecurity.Persistence.entity;

public interface IAdminService
{
    Task<List<RegistrationWithRoleDto>> GetAllRegistrationsAsync();
    Task<Registration> GetRegistrationByIdAsync(int id);
    Task UpdateRegistrationAsync(Registration registration);
    Task<bool> DeleteRegistrationAsync(int id);
}
