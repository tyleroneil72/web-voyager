using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Models;

namespace web_voyager.Controllers;

[Route("Hotel")]
public class HotelController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
