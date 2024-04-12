using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

public class Hotel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Location { get; set; }

    [Required]
    public required string Address { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required int Price { get; set; }

    [Required]
    [Display(Name = "Rooms Available")]
    public required int RoomsAvailable { get; set; }
}