using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_voyager.Data;
using web_voyager.Areas.TravelServices.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace web_voyager.Areas.TravelServices.Controllers;

[Area("TravelServices")]
[Route("Hotel")]
public class HotelController : Controller
{

    private readonly AppDbContext _db;
    private readonly ILogger<HotelController> _logger;

    public HotelController(AppDbContext db, ILogger<HotelController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        _logger.LogInformation("Hotel Index page visited.");
        return View(_db.Hotels.ToList());
    }

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    [HttpGet("Create")]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            _db.Hotels.Add(hotel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(hotel);
    }

    [HttpGet("Edit/{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var hotel = _db.Hotels.Find(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id, [Bind("Id,Name,Location,Address,Description,Price,RoomsAvailable")] Hotel hotel)
    {
        if (id != hotel.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _db.Hotels.Update(hotel);
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(hotel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(hotel);
    }

    private bool HotelExists(int id)
    {
        return _db.Hotels.Any(e => e.Id == id);
    }

    [HttpGet("Delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var hotel = _db.Hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    [HttpPost("DeleteConfirmed/{id:int}"), ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // First, find and delete any hotel bookings related to the hotel
        var relatedHotelBookings = _db.Bookings.Where(b => b.HotelId == id).ToList();
        if (relatedHotelBookings.Any())
        {
            _db.Bookings.RemoveRange(relatedHotelBookings);
            await _db.SaveChangesAsync(); // Save changes after removing hotel bookings
        }

        // Then, find and delete the hotel
        var hotel = await _db.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _db.Hotels.Remove(hotel);
            await _db.SaveChangesAsync(); // Save changes after removing the hotel
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    [HttpGet("Booking/{id:int}")]
    public async Task<IActionResult> Booking(int id)
    {
        var hotel = await _db.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    [HttpPost("SubmitBooking/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitBooking(int id)
    {
        // First, find the hotel based on the ID.
        var hotel = await _db.Hotels.FindAsync(id);
        if (hotel == null)
        {
            // If the hotel doesn't exist, return a NotFound result.
            return NotFound();
        }

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            // Get the user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Check if the user exists in the database
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                // Handle the case where the user is not found. Could redirect to login or show an error.
                return NotFound("User not found.");
            }
            // Create a new booking object.
            var newBooking = new Booking
            {
                ApplicationUserId = userId, // Set the user's ID.
                ApplicationUser = user, // Set the user object.
                HotelId = id, // Set the Hotels ID
                Type = "Hotel",
            };
            hotel.RoomsAvailable -= 1;
            _db.Bookings.Add(newBooking);
            await _db.SaveChangesAsync();

            return RedirectToAction("BookingConfirmation", new { id = newBooking.Id });
        }

        var guestUserId = 1; // Guest Id

        // Check if the user exists in the database
        var guestUser = await _db.GuestUsers.FindAsync(guestUserId);
        if (guestUser == null)
        {
            // Handle the case where the user is not found.
            return NotFound("User not found.");
        }
        // Create a new booking object for the hotel.
        var booking = new Booking
        {
            GuestUserId = guestUserId, // Set the user's ID.
            GuestUser = guestUser, // Set the user object.
            HotelId = id, // Set the hotel's ID.
            Type = "Hotel",
        };

        hotel.RoomsAvailable -= 1;

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return RedirectToAction("BookingConfirmation", new { id = booking.Id });
    }

    [HttpGet("BookingConfirmation/{id:int}")]
    public async Task<IActionResult> BookingConfirmation(int id)
    {
        var booking = await _db.Bookings
                                .Include(b => b.Hotel)
                                .Include(b => b.GuestUser)
                                .FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }


    [HttpGet("Search/{searchString?}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var hotelsQuery = from h in _db.Hotels
                          select h;

        bool searchPerformed = !String.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            hotelsQuery = hotelsQuery.Where(h => h.Name.Contains(searchString) ||
                                                 h.Location.Contains(searchString) ||
                                                 h.Description.Contains(searchString));
        }
        var hotels = await hotelsQuery.ToListAsync();
        ViewData["searchPerformed"] = searchPerformed;
        ViewData["searchString"] = searchString;

        return View("Index", hotels);
    }

}
