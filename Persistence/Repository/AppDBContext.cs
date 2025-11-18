using Microsoft.EntityFrameworkCore;
using InformationSecurity.Persistence.entity;

namespace InformationSecurity.Persistence.Repository
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial roles
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "Admin" },
                new RoleEntity { Id = 2, Name = "User" },
                new RoleEntity { Id = 3, Name = "Guest" }
            );
        }

    }
}
