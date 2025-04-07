using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
			var model = new ProductModel
			{
				Brands = _db.Brands
					.Select(b => new SelectListItem()
					{
						Value = b.IdBrand.ToString(),
						Text = b.Name
					})
					.ToList(),

				Categories = _db.CategoriesOfProducts
					.Select(c => new SelectListItem()
					{
						Value = c.IdCategory.ToString(),
						Text = c.Name
					})
					.ToList()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(ProductModel model)
		{
			var product = (Product)model;

			_db.Products.Add(product);
			_db.SaveChanges();

			int addedProductId = _db.Products.Last().IdProduct;
			return RedirectToAction("Edit", "Product", new { id = addedProductId });
		}

		public IActionResult Edit(int id)
		{
			var product = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();

			var model = (ProductModel)product;

			model.Brands = _db.Brands
				.Select(b => new SelectListItem
				{
					Value = b.IdBrand.ToString(),
					Text = b.Name
				})
				.ToList();

			model.Categories = _db.CategoriesOfProducts
				.Select(c => new SelectListItem
				{
					Value = c.IdCategory.ToString(),
					Text = c.Name
				})
				.ToList();

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(ProductModel model)
		{
			var editedProduct = (Product)model;

			var oldProduct = _db.Products.FirstOrDefault(p => p.IdProduct == editedProduct.IdProduct);
			if (oldProduct == null) return NotFound();

			oldProduct.UpdateFrom(model);

			_db.SaveChanges();
			return RedirectToAction("Edit", "Product", new { id = model.IdProduct });
		}

		public IActionResult Details(int id)
		{
			var product = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Brand)
				.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();

			return View(product);
		}

		public IActionResult Detele(int id)
		{
			var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();
			return View();
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
			if (product == null) return NotFound();

			_db.Products.Remove(product);
			_db.SaveChanges();
			return RedirectToAction("Products", "Product");
		}
    }
}
