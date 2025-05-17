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
    }
}