using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var car = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }
}
