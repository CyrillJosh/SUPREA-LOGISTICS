using Microsoft.AspNetCore.Mvc;
using SUPREA_LOGISTICS.Context;

namespace SUPREA_LOGISTICS.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDBContext _context;
        public UserController(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password && u.IsActive);

            if(user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetString("UserId", email);
            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

    }
}
