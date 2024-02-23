using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_voyager.Data;
using web_voyager.Models;

namespace web_voyager.Controllers;
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
        var project = _db.Flights.FirstOrDefault(f => f.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
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
    public IActionResult DeleteConfirmed(int id)
    {
        var flight = _db.Flights.Find(id);
        if (flight != null)
        {
            _db.Flights.Remove(flight);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    [HttpPost("BookFlight")]
    [ValidateAntiForgeryToken]
    public IActionResult BookFlight(int FlightId)
    {
        var flight = _db.Flights.FirstOrDefault(f => f.Id == FlightId);
        if (flight == null)
        {
            return NotFound();
        }

        var user = _db.Users.Find(1); // User Id of 1 is Guest
        if (user == null)
        {
            return NotFound();
        }

        var booking = new Booking
        {
            UserId = 1, // User Id of 1 is Guest
            User = user!,
            FlightId = FlightId,
            Type = "Flight"
        };

        _db.Bookings.Add(booking);
        _db.SaveChanges();

        return RedirectToAction("Index");
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
