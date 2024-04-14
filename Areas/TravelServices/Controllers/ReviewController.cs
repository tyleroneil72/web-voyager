namespace web_voyager.Areas.TravelServices.Controllers;

using web_voyager.Areas.TravelServices.Models;
using web_voyager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("TravelServices")]
[Route("[area]/[controller]/[action]")]
public class ReviewController : Controller
{
    private readonly AppDbContext _db;

    public ReviewController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(int reviewingId, string type)
    {
        var reviews = await _db.Reviews
            .Where(r => r.ReviewingId == reviewingId && r.ReviewableType == type)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
        return Json(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] Review review)
    {
        if (ModelState.IsValid)
        {
            review.CreatedDate = DateTime.Now;
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Review added successfully." });
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        return Json(new { success = false, message = "Invalid Review Data", errors = errors });
    }

}
