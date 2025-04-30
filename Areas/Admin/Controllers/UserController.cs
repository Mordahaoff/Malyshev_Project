using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly ILogger<UserController> _logger;
		private readonly PostgresContext _db;

		public UserController(ILogger<UserController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult List()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			List<User> users = _db.Users.ToList();
			return View(users);
		}

		public IActionResult Details(int? id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var userById = _db.Users.FirstOrDefault(u => u.IdUser == id);
			if (userById == null) return BadRequest($"User ID:[{id}] is not found.");

			if (!string.IsNullOrEmpty(userById!.Telephone))
			{
				userById.Telephone =
					"+7 " + userById.Telephone.Substring(0, 3)
					+ "-" + userById.Telephone.Substring(3, 3)
					+ "-" + userById.Telephone.Substring(6, 2)
					+ "-" + userById.Telephone.Substring(8, 2);
			}
			
			//userById!.Orders = userById.Orders.Where(o => o.StateOfOrderId != 1).ToList();
			return View(userById);
		}

		public IActionResult Delete(int? id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var userById = _db.Users.FirstOrDefault(u => u.IdUser == id);
			if (userById == null) return BadRequest($"User ID:[{id}] is not found.");

			_db.Users.Remove(userById);
			_db.SaveChanges();

			return RedirectToAction("List", "User", new { area = "Admin" });
		}
	}
}
