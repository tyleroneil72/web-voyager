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
