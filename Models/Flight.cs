using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;

public class Flight
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Departure { get; set; }
    [Required]
    public required string Arrival { get; set; }
    [Required]
    public required DateTime DepartureTime { get; set; }
    [Required]
    public required DateTime ArrivalTime { get; set; }
    [Required]
    public required string Airline { get; set; }
    [Required]
    public required string FlightNumber { get; set; }
    [Required]
    public required string Status { get; set; }
}
