using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Malyshev_Project.Models;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ReviewController : Controller
	{
		private readonly PostgresContext _db;
		private readonly ILogger<ReviewController> _logger;

		public ReviewController(PostgresContext context, ILogger<ReviewController> logger)
		{
			_db = context;
			_logger = logger;
		}

		public IActionResult List(string? userLogin)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var reviews = _db.Reviews
				.Include(r => r.User)
				.Include(r => r.Product)
				.OrderByDescending(r => r.IdReview)
				.ToList();

			if (userLogin != null)
			{
				reviews = reviews.Where(r => r.User.Login == userLogin).ToList();
				ViewData["Request"] = userLogin;
			}

			return View(reviews);
		}

		public IActionResult Delete(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
			if (review == null) return NotFound($"Review [ID:{id}] is not found.");

			_db.Reviews.Remove(review);
			_db.SaveChanges();
			return RedirectToAction("List", "Review");
		}
	}
}
