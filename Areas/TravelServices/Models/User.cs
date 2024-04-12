using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

// ID: 1 = Guest, 2 = Admin
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

}