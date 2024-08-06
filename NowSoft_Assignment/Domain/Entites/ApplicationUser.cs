using Domain.Common;

namespace Domain.Entites
{
    public class ApplicationUser : BaseEntity
    {
        public ApplicationUser()
        {
            //UserRoles = new HashSet<ApplicationUserRole>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string BrowserName { get; set; }
        public string IPAddress { get; set; }
        public double Balance { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
