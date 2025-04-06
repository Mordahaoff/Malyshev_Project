using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Malyshev_Project.Controllers
{
    public class UserController : Controller
    {
		private readonly ILogger<UserController> _logger;
		private readonly PostgresContext _db;

		public UserController(ILogger<UserController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}
		public IActionResult Profile()
        {
			var user = HttpContext.Session.Get<User>("user");

			if (user == null) RedirectToAction("Login", "Auth");

            return View(user);
        }

		public IActionResult Edit()
		{
			var user = HttpContext.Session.Get<User>("user");

			if (user == null) NotFound();

			return View(user);
		}
    }
}
