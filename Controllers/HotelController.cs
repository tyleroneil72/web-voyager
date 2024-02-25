using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_voyager.Data;
using web_voyager.Models;

namespace web_voyager.Controllers;

[Route("Hotel")]
public class HotelController : Controller
{

    private readonly AppDbContext _db;

    public HotelController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
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
