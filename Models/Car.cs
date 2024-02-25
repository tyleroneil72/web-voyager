using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;

public class CarRental
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Brand { get; set; }

    [Required]
    public required string Model { get; set; }

    [Required]
    public required string Location { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required int PricePerDay { get; set; }

    [Required]
    public required int CarsAvailable { get; set; }

    [Required]
    public required int Seats { get; set; }
}
