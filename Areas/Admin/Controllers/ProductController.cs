using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly PostgresContext _db;

		public ProductController(ILogger<ProductController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult List(string? productName)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var products = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.OrderBy(p => p.IdProduct)
				.ToList();

			if (productName != null)
			{
				products = products
					.Where(p => p.Name.ToLower().Contains(productName.ToLower().Trim()))
					.ToList();
			}

			var productUnits = products.Select(p => new ProductUnits(p)).ToList();

			return View(productUnits);
		}

		public IActionResult Create()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var model = new CreateProductModel
			{
				Brands = _db.Brands.ToList(),
				Categories = _db.CategoriesOfProducts.ToList()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(CreateProductModel model)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			_db.Products.Add(model.Product);
			_db.SaveChanges();

			int createdProductId = _db.Products.OrderBy(p => p.IdProduct).Last().IdProduct;

			var storesDb = _db.Stores.ToList();
			foreach (var store in storesDb)
			{
				_db.StoresProducts.Add(new StoresProduct
				{
					ProductId = createdProductId,
					StoreId = store.IdStore,
					CountOfProduct = 0
				});
			}

			_db.SaveChanges();
			return RedirectToAction("Edit", "Product", new { id = createdProductId });
		}

		public IActionResult Edit(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var product = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound($"Product [ID:{id}] is not found.");

			var model = new EditProductModel()
			{
				Product = product,
				Brands = _db.Brands.ToList(),
				Categories = _db.CategoriesOfProducts.ToList()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditProductModel model)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			_db.Products.Update(model.Product);
			_db.SaveChanges();
			return RedirectToAction("Edit", "Product", new { id = model.Product.IdProduct });
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
			var model = new ProductDetailsModel(product);

			return View(model);
		}

		public IActionResult Delete(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound($"Product [ID:{id}] is not found.");

			_db.Products.Remove(product);
			_db.SaveChanges();
			return RedirectToAction("List", "Product");
		}
	}
}
