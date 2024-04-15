using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Models;

namespace web_voyager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Home page visited");
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Privacy page visited");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        _logger.LogInformation("General search for {SearchType} with {SearchString}", searchType, searchString);

        try
        {
            if (searchType == "Flights")
            {
                return Redirect($"/Flight/Search/{searchString}");
            }
            else if (searchType == "Hotels")
            {
                return Redirect($"/Hotel/Search/{searchString}");
            }
            else if (searchType == "Car Rentals")
            {
                return Redirect($"/Car/Search/{searchString}");
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for {SearchType} with {SearchString}", searchType, searchString);
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public IActionResult NotFound(int statusCode)
    {
        _logger.LogInformation("Not found page visited with status code {StatusCode}", statusCode);

        try
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }
            return View("Error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error showing not found page with status code {StatusCode}", statusCode);
            return RedirectToAction("Error", "Home");
        }
    }
}
