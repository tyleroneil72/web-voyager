using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Type { get; set; }


    public GuestUser? GuestUser { get; set; }

    public int? GuestUserId { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }

    public string? ApplicationUserId { get; set; }

    public Flight? Flight { get; set; }
    public int? FlightId { get; set; }

    public int? HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public int? CarId { get; set; }
    public Car? Car { get; set; }
}