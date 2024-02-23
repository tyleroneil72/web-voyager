using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Data;
using web_voyager.Models;

namespace web_voyager.Controllers;

public class FlightController : Controller
{
    private readonly AppDbContext _db;

    public FlightController(AppDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        return View(_db.Flights.ToList());
    }
}
