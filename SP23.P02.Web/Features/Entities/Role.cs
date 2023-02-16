using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.Entities;

namespace SP23.P02.Web.Features.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();

}

