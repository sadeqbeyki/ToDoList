using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Infrastructure.EFCore.Persistance;

namespace ToDo.Infrastructure.EFCore.Seed;

public static class IdentityDataSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Manager", "Member" };

        // Create roles if they don't exist
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create default admin user
        string adminEmail = "admin@example.com";
        string password = "123456";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, password);
            if (!result.Succeeded)
                throw new Exception("Cant make admin user!");
        }

        // Add roles to admin
        foreach (var role in roles)
        {
            if (!await userManager.IsInRoleAsync(adminUser, role))
            {
                await userManager.AddToRoleAsync(adminUser, role);
            }
        }
    }
}
