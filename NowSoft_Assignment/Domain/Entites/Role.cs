using Domain.Common;

namespace Domain.Entites
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
        }

        public string Name { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
