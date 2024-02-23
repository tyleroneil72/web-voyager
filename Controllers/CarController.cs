using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Models;

namespace web_voyager.Controllers;
[Route("Car")]
public class CarController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
