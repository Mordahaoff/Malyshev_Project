using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
			var user = HttpContext.Session.Get<User>("user"); // авторизованный пользователь
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

			if (!string.IsNullOrEmpty(userDb!.Telephone))
			{
				userDb.Telephone =
					"+7 " + userDb.Telephone.Substring(0, 3)
					+ "-" + userDb.Telephone.Substring(3, 3)
					+ "-" + userDb.Telephone.Substring(6, 2)
					+ "-" + userDb.Telephone.Substring(8, 2);
			}
			userDb!.Orders = userDb.Orders.Where(o => o.StateOfOrderId != 1).OrderByDescending(o => o.DateOfStatusChange).ToList();
			return View(userDb);
		}

		[HttpPost]
		public IActionResult Profile(User user)
		{
			user.Telephone = user.Telephone?.Replace("+7", "").Replace("-", "").Replace(" ", "");
			if (string.IsNullOrEmpty(user.Photo)) user.Photo = "https://cdn-icons-png.flaticon.com/512/149/149071.png";
			_db.Users.Update(user);
			_db.SaveChanges();
			HttpContext.Session.Set("user", user);
			return RedirectToAction("Profile", "User");
		}
	}
}
