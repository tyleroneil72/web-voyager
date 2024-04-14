using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web_voyager.Data;
using System.Threading.Tasks;
using web_voyager.Areas.TravelServices.Models;

namespace web_voyager.Areas.Identity.Pages.Account.Manage;
public class ConfirmCancelModel : PageModel
{
    private readonly AppDbContext _context;

    public ConfirmCancelModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Booking? Booking { get; set; }

    public async Task<IActionResult> OnGetAsync(int bookingId)
    {
        Booking = await _context.Bookings.FindAsync(bookingId);
        if (Booking == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (Booking == null || Booking.Id == 0)
            {
                return NotFound("Booking not found or ID not provided.");
            }

            var booking = await _context.Bookings.FindAsync(Booking.Id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            booking.IsCancelled = true;  // Mark the booking as canceled

            if (booking.FlightId != null)
            {
                var flight = await _context.Flights.FindAsync(booking.FlightId);
                if (flight != null)
                {
                    flight.SeatsAvailable++;
                }
            }

            if (booking.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(booking.HotelId);
                if (hotel != null)
                {
                    hotel.RoomsAvailable++;
                }
            }

            if (booking.CarId != null)
            {
                var car = await _context.Cars.FindAsync(booking.CarId);
                if (car != null)
                {
                    car.CarsAvailable++;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./MyBookings");
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework or the built-in logger)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
