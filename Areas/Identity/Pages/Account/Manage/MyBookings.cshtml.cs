using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using web_voyager.Areas.TravelServices.Models;
using web_voyager.Data;

namespace web_voyager.Areas.Identity.Pages.Account.Manage;
public class MyBookingsModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public MyBookingsModel(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public List<Booking>? Bookings { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            // Check if the user is an admin
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                // Admin user: Fetch all bookings
                Bookings = await _context.Bookings
                                    .Include(b => b.Flight)
                                    .Include(b => b.Hotel)
                                    .Include(b => b.Car)
                                    .Include(b => b.ApplicationUser)
                                    .Where(b => b.IsCancelled != true)
                                    .ToListAsync();
            }
            else
            {
                // Regular user: Fetch only user-specific bookings
                Bookings = await _context.Bookings
                                    .Include(b => b.Flight)
                                    .Include(b => b.Hotel)
                                    .Include(b => b.Car)
                                    .Include(b => b.ApplicationUser)
                                    .Where(b => b.ApplicationUserId == user.Id && b.IsCancelled != true)
                                    .ToListAsync();
            }
        }
    }

}
