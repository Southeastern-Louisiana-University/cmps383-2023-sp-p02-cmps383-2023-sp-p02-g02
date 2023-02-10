using Microsoft.AspNetCore.Identity;

namespace SP23.P02.Web.Features.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> Users { get; set; } = new List<UserRole>();

    }

    public class RoleType 
    {
        public const string Admin = nameof(Admin);

        public const string User = nameof(User);
    }



}
