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

			if (user == null) return NotFound();

			return View(user);
		}

		[HttpPost]
		public IActionResult Edit(User editedUser)
		{
			var sessionUser = HttpContext.Session.Get<User>("user");
			if (sessionUser == null) return NotFound();

			var dbUser = _db.Users.First(u => u.IdUser == sessionUser!.IdUser);
			dbUser.Login = editedUser.Login;
			dbUser.Password = editedUser.Password;
			//dbUser.RoleId = editedUser.RoleId;
			dbUser.Surname = editedUser.Surname;
			dbUser.Name = editedUser.Name;
			dbUser.Patronymic = editedUser.Patronymic;
			dbUser.Gender = editedUser.Gender;
			dbUser.Telephone = editedUser.Telephone;
			dbUser.Photo = editedUser.Photo;
			_db.SaveChanges();

			HttpContext.Session.Set("user", dbUser);

			return RedirectToAction("Edit", "User");
		}
	}
}
