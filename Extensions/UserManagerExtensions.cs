using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using web_voyager.Areas.TravelServices.Models;  // Ensure this uses the correct namespace where ApplicationUser is defined

namespace web_voyager.Extensions;

public static class UserManagerExtensions
{
    public static string GetFirstName(this UserManager<ApplicationUser> userManager, ApplicationUser user)
    {
        return user.FirstName;
    }
}
