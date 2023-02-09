using Microsoft.AspNetCore.Identity;
using System.Data;

namespace SP23.P02.Web.User_Account_Authorizations
{
    public class UserRole : IdentityUserRole<int>
    {      
            public virtual Role? Role { get; set; }
            public virtual User? User { get; set; }
        


    }
}
