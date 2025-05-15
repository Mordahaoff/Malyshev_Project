using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project.Controllers
{
	public class ProductController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly PostgresContext _db;

		public ProductController(ILogger<ProductController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult Details(int id)
		{
			var product = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.Include(p => p.StoresProducts)
					.ThenInclude(sp => sp.Store)
						.ThenInclude(s => s.Address)
				.Include(p => p.Reviews)
					.ThenInclude(r => r.User)
				.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound($"Product [ID:{id}] is not found.");

			product.Reviews = product.Reviews.OrderByDescending(r => r.IdReview).ToList();

			var model = new ProductDetailsModel()
			{
				Product = product
			};

			return View(model);
		}
	}
}
