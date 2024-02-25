using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Data;
using web_voyager.Models;

namespace web_voyager.Controllers;
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
}
