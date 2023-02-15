using Microsoft.AspNetCore.Identity;

namespace SP23.P02.Web.Features.Entities;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> Roles { get; set;  } = new List<UserRole>();
}
