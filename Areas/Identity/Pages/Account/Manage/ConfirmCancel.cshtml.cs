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
        var booking = await _context.Bookings.FindAsync(Booking.Id);
        if (booking == null)
        {
            return NotFound();
        }

        booking.IsCancelled = true;  // Mark the booking as canceled
        await _context.SaveChangesAsync();
        return RedirectToPage("./BookingsList");  // Redirect to the bookings list or another appropriate page
    }
}
