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
        var car = await _db.Cars.FirstOrDefaultAsync(h => h.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
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
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Location,Description,PricePerDay,CarsAvailable")] Car car)
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
    public IActionResult Delete(int id)
    {
        var car = _db.Cars.FirstOrDefault(f => f.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
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
