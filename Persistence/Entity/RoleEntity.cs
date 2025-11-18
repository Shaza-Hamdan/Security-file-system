namespace InformationSecurity.Persistence.entity
{
    public class RoleEntity
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Role Name (e.g., Admin, User, Guest)

        // Navigation property for associated users
        public ICollection<Registration> Registrations { get; set; }
    }
}