using System.ComponentModel.DataAnnotations;

namespace web_voyager.Areas.TravelServices.Models;

public class Review
{
    public int ReviewId { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Content cannot be longer than 500 characters.")]
    public required string Content { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    public int ReviewingId { get; set; }

    [Required]
    public required string ReviewableType { get; set; } // "Flight", "Car", or "Hotel"
}
