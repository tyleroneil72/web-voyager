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

    [HttpGet("Search/{searchString?}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var flightsQuery = from f in _db.Flights
                           select f;

        bool searchPerformed = !String.IsNullOrEmpty(searchString);

        if (searchPerformed)
        {
            // Assuming you want to search by both Departure and Arrival or any other fields
            flightsQuery = flightsQuery.Where(f => f.Departure.Contains(searchString) || f.Arrival.Contains(searchString) || f.Airline.Contains(searchString));
        }
        var flights = await flightsQuery.ToListAsync();
        ViewData["searchPerformed"] = searchPerformed;
        ViewData["searchString"] = searchString;

        return View("Index", flights); // Make sure the view is expecting a list of flights
    }
}
