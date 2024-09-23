using Microsoft.AspNetCore.Identity;

namespace ToDo.Domain.Entities.UserAgg;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}
