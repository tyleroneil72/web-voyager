using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_voyager.Data;
using web_voyager.Areas.TravelServices.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace web_voyager.Areas.TravelServices.Controllers;

[Area("TravelServices")]
[Route("Car")]
public class CarController : Controller
{
    private readonly AppDbContext _db;

    public CarController(AppDbContext db)
    {
        _db = db;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        return View(_db.Cars.ToList());
    }

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var car = await _db.Cars.FirstOrDefaultAsync(h => h.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
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
    public async Task<IActionResult> Create(Car car)
    {
        if (ModelState.IsValid)
        {
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(car);
    }

    [HttpGet("Edit/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Location,Description,PricePerDay,Seats,CarsAvailable")] Car car)
    {
        if (id != car.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        return View(car);
    }

    private bool CarExists(int id)
    {
        return _db.Cars.Any(e => e.Id == id);
    }

    [HttpGet("Delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var car = _db.Cars.FirstOrDefault(f => f.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    [HttpPost("DeleteConfirmed/{id:int}"), ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // First, find and delete any car bookings related to the car
        var relatedBookings = _db.Bookings.Where(b => b.CarId == id).ToList();
        if (relatedBookings.Any())
        {
            _db.Bookings.RemoveRange(relatedBookings);
            await _db.SaveChangesAsync(); // Save changes after removing bookings
        }

        // Then, find and delete the car
        var car = await _db.Cars.FindAsync(id);
        if (car != null)
        {
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync(); // Save changes after removing the car
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    [HttpGet("Booking/{id:int}")]
    public async Task<IActionResult> Booking(int id)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    [HttpPost("SubmitBooking/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitBooking(int id)
    {
        // First, find the car based on the ID.
        var car = await _db.Cars.FindAsync(id);
        if (car == null)
        {
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
                CarId = id, // Set the Cars ID
                Type = "Car",
            };
            car.CarsAvailable -= 1;
            _db.Bookings.Add(newBooking);
            await _db.SaveChangesAsync();

            return RedirectToAction("BookingConfirmation", new { id = newBooking.Id });
        }


        var guestUserId = 1; // Guest Id

        var guestUser = await _db.GuestUsers.FindAsync(guestUserId);
        if (guestUser == null)
        {
            return NotFound("User not found.");
        }

        var booking = new Booking
        {
            GuestUserId = guestUserId,
            GuestUser = guestUser,
            CarId = id,
            Type = "Car",
        };

        car.CarsAvailable -= 1;
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return RedirectToAction("BookingConfirmation", new { id = booking.Id });
    }

    [HttpGet("BookingConfirmation/{id:int}")]
    public async Task<IActionResult> BookingConfirmation(int id)
    {
        var booking = await _db.Bookings
                                .Include(b => b.Car)
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
        var carsQuery = from c in _db.Cars
                        select c;

        bool searchPerformed = !String.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            carsQuery = carsQuery.Where(c => c.Brand.Contains(searchString) ||
                                             c.Model.Contains(searchString) ||
                                             c.Location.Contains(searchString) ||
                                             c.Description.Contains(searchString));
        }
        var cars = await carsQuery.ToListAsync();
        ViewData["searchPerformed"] = searchPerformed;
        ViewData["searchString"] = searchString;

        return View("Index", cars);
    }

}
