using Microsoft.AspNetCore.Identity;
using web_voyager.Areas.TravelServices.Models;

namespace web_voyager.Models;

public class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Enum.Roles.User.ToString()));
    }
}