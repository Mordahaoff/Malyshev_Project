using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

			var userDb = _db.Users
				.Include(u => u.Role)
				.Include(u => u.Orders)
					.ThenInclude(o => o.StateOfOrder)
				.Include(u => u.Orders)
					.ThenInclude(o => o.Store)
						.ThenInclude(s => s.Address)
				.Include(o => o.Orders)
					.ThenInclude(o => o.OrdersProducts)
						.ThenInclude(op => op.Product)
				.FirstOrDefault(u => u.IdUser == user.IdUser);
            return View(userDb);
        }

		[HttpPost]
		public IActionResult MyProfile(User user)
		{
			_db.Users.Update(user);
			_db.SaveChanges();
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
