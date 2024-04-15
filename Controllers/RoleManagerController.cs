using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace web_voyager.Controllers;

public class RoleManagerController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleManagerController> _logger;

    public RoleManagerController(RoleManager<IdentityRole> roleManager, ILogger<RoleManagerController> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Role Manager Index Page Visited");
        try
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting roles");
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRoles(string roleName)
    {
        _logger.LogInformation("Adding role {RoleName}", roleName);

        try
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding role {RoleName}", roleName);
            return RedirectToAction("Error", "Home");
        }
    }
}

