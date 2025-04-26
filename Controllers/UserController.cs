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
		public IActionResult MyProfile()
        {
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");
            return View(user);
        }

		public IActionResult Edit()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");
			return View(user);
		}

		[HttpPost]
		public IActionResult Edit(User editedUser)
		{
			_db.Users.Update(editedUser);
			_db.SaveChanges();
			HttpContext.Session.Set("user", editedUser);
			return RedirectToAction("Edit", "User");
		}
	}
}
