namespace InformationSecurity.DTO
{
    public record RegistrationWithRoleDto
    (
          int Id,
          string UserName,
          string Email,
          string PhoneNumber,
          string RoleName // To hold the Role.Name
    );
}