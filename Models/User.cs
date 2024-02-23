using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;
// Temporary outline
// ID: 1 = Guest, 2 = Admin
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Role { get; set; }

    [Required]
    public required string Status { get; set; }

    public List<Flight>? Flights { get; set; }
}