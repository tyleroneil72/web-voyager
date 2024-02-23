using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;

// ID: 1 = Guest, 2 = Admin
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

    public List<Flight>? Flights { get; set; }

    // public List<Hotel>? Hotels { get; set; }

    // public List<Car>? Cars { get; set; }

}