using web_voyager.Areas.TravelServices.Models;
using web_voyager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace web_voyager.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRolesController> _logger;

        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<UserRolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        private async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User Roles Index Page Visited");

            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userRolesViewModel = new List<UserRolesViewModel>();

                foreach (ApplicationUser u in users)
                {
                    var thisViewModel = new UserRolesViewModel
                    {
                        UserId = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email ?? string.Empty,
                        Roles = await GetUserRolesAsync(u)
                    };
                    userRolesViewModel.Add(thisViewModel);
                }

                return View(userRolesViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user roles");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(string userId)
        {
            _logger.LogInformation("Manage User Roles Page Visited");

            try
            {
                ViewBag.userId = userId;
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                    return View("NotFound");
                }

                ViewBag.UserName = user.UserName;
                var userRoles = await _userManager.GetRolesAsync(user);
                var model = new List<ManageUserRolesViewModel>();
                foreach (var role in _roleManager.Roles)
                {
                    var userRolesViewModel = new ManageUserRolesViewModel
                    {
                        RolesId = role.Id,
                        RoleName = role.Name ?? "DefaultRoleName",
                        Selected = userRoles.Contains(role.Name ?? string.Empty)
                    };
                    model.Add(userRolesViewModel);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error managing user roles");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            _logger.LogInformation("Manage User Roles Page Visited");

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return View();
                }

                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user from roles");
                    return View(model);
                }

                result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add user to roles");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error managing user roles");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
