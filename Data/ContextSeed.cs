using Microsoft.AspNetCore.Identity;
using web_voyager.Areas.TravelServices.Models;

namespace web_voyager.Models;

public class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Traveler.ToString()));
    }

    public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {

        var adminUser = new ApplicationUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            FirstName = "Web",
            LastName = "Voyager",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != adminUser.Id))
        {
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(adminUser, "[123Pa$$word.]"); // This Password is for Development Purposes Only
                await userManager.AddToRoleAsync(adminUser, Enum.Roles.Traveler.ToString());
                await userManager.AddToRoleAsync(adminUser, Enum.Roles.Admin.ToString());
            }
        }
    }
}