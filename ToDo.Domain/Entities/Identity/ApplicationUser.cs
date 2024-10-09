using Microsoft.AspNetCore.Identity;

namespace ToDo.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }

}