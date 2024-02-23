using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required User User { get; set; }

    [Required]
    public required int UserId { get; set; }

    public Flight? Flight { get; set; }
    public int? FlightId { get; set; }

    // public int? HotelId { get; set; }
    // public Hotel? Hotel { get; set; }

    // public int? CarId { get; set; }
    // public Car? Car { get; set; }
}