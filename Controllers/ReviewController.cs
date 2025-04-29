using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Malyshev_Project.Models;

namespace Malyshev_Project.Controllers
{
	public class ReviewController : Controller
	{
		private readonly PostgresContext _db;
		private readonly ILogger<ReviewController> _logger;

		public ReviewController(PostgresContext context, ILogger<ReviewController> logger)
		{
			_db = context;
			_logger = logger;
		}

		public IActionResult List()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			List<Review> reviews = _db.Review
				.Include(r => r.User)
				.Include(r => r.Product)
				.ToList();

			return View(reviews);
		}

		public IActionResult Delete(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var review = _db.Stores.FirstOrDefault(s => s.IdStore == id);
			if (review == null) return BadRequest($"Store [ID:{id}] is not found.");

			_db.Review.Remove(review);
			return RedirectToAction("List", "Review");
		}
	}
}
