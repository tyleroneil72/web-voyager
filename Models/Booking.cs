using System.ComponentModel.DataAnnotations;

namespace web_voyager.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required User User { get; set; }

    public Flight? Flight { get; set; }
    // public Hotel? Hotel { get; set; }
    // public Car? Car { get; set; }
}