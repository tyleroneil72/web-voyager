using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

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
    public required string Status { get; set; }

    [Required]
    public required int Capacity { get; set; }

    [Required]
    [Display(Name = "Seats Available")]
    public required int SeatsAvailable { get; set; }

    [Required]
    public required int Price { get; set; }

}
