using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP23.P02.Web.Features.Entities;

namespace SP23.P02.Web.Features.Entities
{

    public class UserRole : IdentityUserRole<int>
    {
        public Role? Role { get; set; }
        public User? User { get; set; }
    }
}