using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;
// Temporary outline
public class Flight
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Departure { get; set; }

    [Required]
    public required string Arrival { get; set; }

    [Required]
    [Display(Name = "Departure Time")]
    public required DateTime DepartureTime { get; set; }

    [Required]
    [Display(Name = "Arrival Time")]
    public required DateTime ArrivalTime { get; set; }

    [Required]
    public required string Airline { get; set; }

    [Required]
    [Display(Name = "Flight Number")]
    public required string FlightNumber { get; set; }

    [Required]
    public required string Status { get; set; }

    public List<User>? Users { get; set; }
}
