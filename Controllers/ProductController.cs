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

		public IActionResult List()
		{
			List<Product> products = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.ToList();
			return View(products);
		}

		public IActionResult Create()
		{
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
			var product = (Product)model;

			_db.Products.Add(product);
			_db.SaveChanges();

			int createdProductId = _db.Products.OrderBy(p => p.IdProduct).Last().IdProduct;
			return RedirectToAction("Edit", "Product", new { id = createdProductId });
		}

		public IActionResult Edit(int id)
		{
			var product = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();

			var model = (EditProductModel)product;
			model.Brands = _db.Brands.ToList();
			model.Categories = _db.CategoriesOfProducts.ToList();

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditProductModel model)
		{
			var oldProduct = _db.Products.FirstOrDefault(p => p.IdProduct == model.IdProduct);
			if (oldProduct == null) return NotFound();

			oldProduct.Name = model.Name;
			oldProduct.Photo = model.Photo;
			oldProduct.Description = model.Description;
			oldProduct.Price = model.Price;
			oldProduct.Volume = model.Volume;
			oldProduct.CategoryId = model.CategoryId;
			oldProduct.BrandId = model.BrandId;

			_db.SaveChanges();
			return RedirectToAction("Edit", "Product", new { id = model.IdProduct });
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
			if (product == null) return NotFound();

			var user = HttpContext.Session.Get<User>("user");
			var model = new ProductDetailsModel(product, user);

			return View(model);
		}

		public IActionResult Detele(int id)
		{
			var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();
			return View(product);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();

			_db.Products.Remove(product);
			_db.SaveChanges();
			return RedirectToAction("List", "Product");
		}
    }
}
