using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace web_voyager.Areas.TravelServices.Models;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public byte[]? ProfilePicture { get; set; }

}