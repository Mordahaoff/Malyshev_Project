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

		// Метод для создания отзыва со страницы "Product/Details/{id?}
		[HttpPost]
		public IActionResult Create(Review review)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return BadRequest("You are not authorized.");

			_db.Reviews.Add(review);
			_db.SaveChanges();

			var createdReview = _db.Reviews.OrderBy(r => r.IdReview).Last();
			_logger.LogInformation($"Created New Review" +
				$"\nUserID: [{createdReview.UserId}]" +
				$"\nProductID: [{createdReview.ProductId}]" +
				$"\nText: [{createdReview.Text}]" +
				$"\nDate of Creation: [{createdReview.DateOfCreation}]");
			return RedirectToAction("Details", "Product", new { id = review.ProductId });
		}

		public IActionResult Delete(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
			if (review == null) return NotFound($"Review [ID:{id}] is not found");
			_db.Reviews.Remove(review);
			_db.SaveChanges();

			return Redirect(HttpContext.Request.Headers.Referer.ToString());
		}

		//public IActionResult List(int? userId)
		//{
		//	List<Review> reviews = _db.Reviews.Include(r => r.User).Include(r => r.Product).ToList();
		//	User? authUser = HttpContext.Session.Get<User>("user");
		//	if (authUser == null) return RedirectToAction("Login", "Auth");

		//	switch (authUser.RoleId)
		//	{
		//		case 1: // Клиент
		//			{
		//				if (userId == null)
		//				{
		//					reviews = _db.Reviews
		//						.Include(r => r.User)
		//						.Include(r => r.Product)
		//						.Where(r => r.UserId == authUser.IdUser)
		//						.ToList();
		//					break;
		//				}
		//				else
		//				{
		//					return BadRequest();
		//				}
		//			}
		//		case 2: // Админ
		//			{
		//				if (userId == null)
		//				{
		//					reviews = _db.Reviews
		//						.Include(r => r.User)
		//						.Include(r => r.Product)
		//						.ToList();
		//				}
		//				else
		//				{
		//					if (_db.Users.Any(u => u.IdUser == userId))
		//					{
		//						reviews = _db.Reviews
		//						.Include(r => r.User)
		//						.Include(r => r.Product)
		//						.Where(r => r.UserId == userId)
		//						.ToList();
		//					}
		//					else
		//					{
		//						return BadRequest();
		//					}
		//				}
		//				break;
		//			}
		//	}

		//	return View(reviews);
		//}

		//public IActionResult Details(int id)
		//{
		//	var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
		//	if (review == null) return NotFound();
		//	return View(review);
		//}

		//public IActionResult Edit(int id)
		//{
		//	var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
		//	if (review == null) return NotFound();
		//	return View(review);
		//}
	}
}
