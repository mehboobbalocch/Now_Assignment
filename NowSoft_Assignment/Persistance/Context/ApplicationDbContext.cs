using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }

        public async Task<int> SaveChangesAsync()
            {
                return await base.SaveChangesAsync();
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserAndRoleSeeding(modelBuilder);
        }

        private void UserAndRoleSeeding(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(
               new Role { Id = Guid.NewGuid(), Name = "Admin" },
               new Role { Id = Guid.NewGuid(), Name = "Customer" }
            );
        }
    }
}
