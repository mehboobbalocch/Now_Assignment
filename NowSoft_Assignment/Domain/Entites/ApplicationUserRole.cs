using Domain.Common;

namespace Domain.Entites
{
    public class ApplicationUserRole : BaseEntity
    {
        public ApplicationUserRole()
        {
            //Role = new();
            //User = new();   
        }
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
