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
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(AppDbContext db, ILogger<ReviewController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(int reviewingId, string type)
    {
        _logger.LogInformation("Getting reviews for {ReviewingId} of type {Type}", reviewingId, type);
        try
        {
            var reviews = await _db.Reviews
                .Where(r => r.ReviewingId == reviewingId && r.ReviewableType == type)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
            return Json(reviews);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting reviews for {ReviewingId} of type {Type}", reviewingId, type);
            return Json(new { success = false, message = "Error getting reviews." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] Review review)
    {
        _logger.LogInformation("Adding review for {ReviewingId} of type {Type}", review.ReviewingId, review.ReviewableType);

        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding review for {ReviewingId} of type {Type}", review.ReviewingId, review.ReviewableType);
            return Json(new { success = false, message = "Error adding review." });
        }
    }
}