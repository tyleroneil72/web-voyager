using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_voyager.Data;
using web_voyager.Areas.TravelServices.Models;

namespace web_voyager.Areas.TravelServices.Controllers;

[Area("TravelServices")]
[Route("Flight")]
public class FlightController : Controller
{
    private readonly AppDbContext _db;

    public FlightController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View(_db.Flights.ToList());
    }

    [HttpGet("Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var flight = _db.Flights.FirstOrDefault(f => f.Id == id);
        if (flight == null)
        {
            return NotFound();
        }
        return View(flight);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Flight flight)
    {
        if (ModelState.IsValid)
        {
            _db.Flights.Add(flight);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(flight);
    }

    [HttpGet("Edit/{id:int}")]
    public IActionResult Edit(int id)
    {
        var flight = _db.Flights.Find(id);
        if (flight == null)
        {
            return NotFound();
        }
        return View(flight);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Departure,Arrival,DepartureTime,ArrivalTime,Airline,Status,Capacity,SeatsAvailable,Price")] Flight flight)
    {
        if (id != flight.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _db.Flights.Update(flight);
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(flight.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(flight);
    }

    private bool FlightExists(int id)
    {
        return _db.Flights.Any(e => e.Id == id);
    }

    [HttpGet("Delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var flight = _db.Flights.FirstOrDefault(f => f.Id == id);
        if (flight == null)
        {
            return NotFound();
        }
        return View(flight);
    }

    [HttpPost("DeleteConfirmed/{id:int}"), ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // First, find and delete any bookings related to the flight
        var relatedBookings = _db.Bookings.Where(b => b.FlightId == id).ToList();
        if (relatedBookings.Any())
        {
            _db.Bookings.RemoveRange(relatedBookings);
            await _db.SaveChangesAsync(); // Save changes after removing bookings
        }

        // Then, find and delete the flight
        var flight = await _db.Flights.FindAsync(id);
        if (flight != null)
        {
            _db.Flights.Remove(flight);
            await _db.SaveChangesAsync(); // Save changes after removing the flight
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    [HttpGet("Booking/{id:int}")]
    public async Task<IActionResult> Booking(int id)
    {
        var flight = await _db.Flights.FindAsync(id);
        if (flight == null)
        {
            return NotFound();
        }
        return View(flight);
    }

    [HttpPost("SubmitBooking/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitBooking(int id)
    {
        // First, find the flight based on the ID.
        var flight = await _db.Flights.FindAsync(id);
        if (flight == null)
        {
            // If the flight doesn't exist, return a NotFound result.
            return NotFound();
        }
        var userId = 1; // Guest Id
        // Check if the user exists in the database
        var user = await _db.Users.FindAsync(userId);
        if (user == null)
        {
            // Handle the case where the user is not found. Could redirect to login or show an error.
            return NotFound("User not found.");
        }
        // Create a new booking object.
        var booking = new Booking
        {
            UserId = userId, // Set the user's ID.
            User = user, // Set the user object.
            FlightId = id, // Set the flight's ID.
            Type = "Flight",

        };
        flight.SeatsAvailable -= 1;
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return RedirectToAction("BookingConfirmation", new { id = booking.Id });
    }

    [HttpGet("BookingConfirmation/{id:int}")]
    public async Task<IActionResult> BookingConfirmation(int id)
    {
        var booking = await _db.Bookings
                                .Include(b => b.Flight)
                                .Include(b => b.User)
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
        var flightsQuery = from f in _db.Flights
                           select f;

        bool searchPerformed = !String.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            flightsQuery = flightsQuery.Where(f => f.Departure.Contains(searchString) || f.Arrival.Contains(searchString) || f.Airline.Contains(searchString));
        }
        var flights = await flightsQuery.ToListAsync();
        ViewData["searchPerformed"] = searchPerformed;
        ViewData["searchString"] = searchString;

        return View("Index", flights);
    }
}
