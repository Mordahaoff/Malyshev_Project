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
		public IActionResult Profile(int? id)
		{
			var user = HttpContext.Session.Get<User>("user"); // авторизованный пользователь
			if (user == null) return RedirectToAction("Login", "Auth");

			if (id == null) // если свой профиль, то
			{
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
				userDb!.Orders = userDb.Orders.Where(o => o.StateOfOrderId != 1).ToList();
				return View(userDb);
			}
			else if (_db.Users.Any(u => u.IdUser == id) && user.RoleId == 2) // если админ авторизован и пользователь существует
			{
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
					.FirstOrDefault(u => u.IdUser == id);

				if (!string.IsNullOrEmpty(userDb!.Telephone))
				{
					userDb.Telephone =
						"+7 " + userDb.Telephone.Substring(0, 3)
						+ "-" + userDb.Telephone.Substring(3, 3)
						+ "-" + userDb.Telephone.Substring(6, 2)
						+ "-" + userDb.Telephone.Substring(8, 2);
				}
				userDb!.Orders = userDb.Orders.Where(o => o.StateOfOrderId != 1).ToList();
				return View(userDb);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		public IActionResult Profile(User user)
		{
			user.Telephone = user.Telephone?.Replace("+7", "").Replace("-", "").Replace(" ", "");
			_db.Users.Update(user);
			_db.SaveChanges();
			HttpContext.Session.Set("user", user);
			return RedirectToAction("MyProfile", "User");
		}

		public IActionResult Delete(int id)
		{
			var user = _db.Users.FirstOrDefault(u => u.IdUser == id);
			if (user == null) return NotFound();
			_db.Users.Remove(user);
			_db.SaveChanges();

			// Получение URL страницы, с которой пришёл запрос
			var referer = HttpContext.Request.Headers.Referer.ToString();
			return Redirect(referer);
		}

		//public IActionResult Edit()
		//{
		//	var user = HttpContext.Session.Get<User>("user");
		//	if (user == null) return RedirectToAction("Login", "Auth");
		//	return View(user);
		//}

		//[HttpPost]
		//public IActionResult Edit(User user)
		//{
		//	_db.Users.Update(user);
		//	_db.SaveChanges();
		//	HttpContext.Session.Set("user", user);
		//	return RedirectToAction("Edit", "User");
		//}
	}
}
