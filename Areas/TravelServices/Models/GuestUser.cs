using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

public class GuestUser
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Username { get; set; }
}