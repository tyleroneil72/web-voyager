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
        return View();
    }

    public IActionResult Privacy()
    {
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
        if (searchType == "Flights")
        {
            return Redirect($"/Flight/Search/{searchString}");
        }
        else if (searchType == "Hotels")
        {
            return Redirect($"/Hotel/Search/{searchString}");
        }
        else if (searchType == "Cars")
        {
            return Redirect($"/Car/Search/{searchString}");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult NotFound(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("NotFound");
        }
        return View("Error");
    }
}
