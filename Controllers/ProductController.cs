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
		public IActionResult Create(CreateProductModel model)
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

			var model = (EditProductModel)product;

			model.Brands = _db.Brands
				.Select(b => new SelectListItem
				{
					Value = b.IdBrand.ToString(),
					Text = b.Name,
					Selected = b.IdBrand == product.BrandId
				})
				.ToList();


			model.Categories = _db.CategoriesOfProducts
				.Select(c => new SelectListItem
				{
					Value = c.IdCategory.ToString(),
					Text = c.Name,
					Selected = c.IdCategory == product.CategoryId
				})
				.ToList();

			//model.Brands.Find(b => b.Value == product.BrandId.ToString())!.Selected = true;
			//model.Categories.Find(c => c.Value == product.CategoryId.ToString())!.Selected = true;

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
			oldProduct.CategoryId = model.Category!.IdCategory;
			oldProduct.BrandId = model.Brand!.IdBrand;

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
