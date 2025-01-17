using Microsoft.AspNetCore.Mvc;
using TrayInventoryApp.Services;

namespace TrayInventoryApp.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: /Admin/Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            // Check if the user is already logged in
            if (HttpContext.Session.GetString("AdminUserName") != null)
            {
                return RedirectToAction("TrayInventory", "TrayInventory");  // Already logged in, go to inventory
            }
            return View();
        }

        // POST: /Admin/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var admin = await _adminService.AuthenticateAdminAsync(username, password);

            if (admin == null)
            {
                // Pass the failure message to the view
                ViewBag.ErrorMessage = "Login failed. Please check your username and password.";
                return View();  // Return to the login page with the error message
            }

            // Store the admin information in the session
            HttpContext.Session.SetString("AdminUserName", username);

            // Redirect to TrayInventory page after successful login
            return RedirectToAction("TrayInventory", "TrayInventory");
        }


    }
}
