using Microsoft.AspNetCore.Identity;

namespace SP23.P02.Web.User_Account_Authorizations
{
    public class User : IdentityUser<int>
    {

        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public string UserName { get; set; } = string.Empty;

    }
}
