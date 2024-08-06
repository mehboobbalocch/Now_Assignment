using Microsoft.EntityFrameworkCore;
using Domain.Entites;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        Task<int> SaveChangesAsync();
    }
}
