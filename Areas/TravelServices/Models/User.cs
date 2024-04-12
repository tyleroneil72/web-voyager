using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace web_voyager.Areas.TravelServices.Models;

// ID: 1 = Guest, 2 = Admin
public class User
{
    [Required]
    public required string FirstName { get; set; }
    [Required]

    public required string LastName { get; set; }

    public byte[]? ProfilePicture { get; set; }

}