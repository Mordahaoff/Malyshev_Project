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

		public IActionResult List(int? userId)
		{
			List<Review> reviews = _db.Reviews.Include(r => r.User).Include(r => r.Product).ToList();
			User? authUser = HttpContext.Session.Get<User>("user");
			if (authUser == null) return RedirectToAction("Login", "Auth");

			switch (authUser.RoleId)
			{
				case 1: // Клиент
					{
						if (userId == null)
						{
							reviews = _db.Reviews
								.Include(r => r.User)
								.Include(r => r.Product)
								.Where(r => r.UserId == authUser.IdUser)
								.ToList();
							break;
						}
						else
						{
							return BadRequest();
						}
					}
				case 2: // Админ
					{
						if (userId == null)
						{
							reviews = _db.Reviews
								.Include(r => r.User)
								.Include(r => r.Product)
								.ToList();
						}
						else
						{
							if (_db.Users.Any(u => u.IdUser == userId))
							{
								reviews = _db.Reviews
								.Include(r => r.User)
								.Include(r => r.Product)
								.Where(r => r.UserId == userId)
								.ToList();
							}
							else
							{
								return BadRequest();
							}
						}
						break;
					}
			}

			return View(reviews);
		}

		[HttpPost]
		public IActionResult Create(Review review)
		{
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

		public IActionResult Details(int id)
		{
			var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
			if (review == null) return NotFound();
			return View(review);
		}

		//public IActionResult Edit(int id)
		//{
		//	var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
		//	if (review == null) return NotFound();
		//	return View(review);
		//}

		public IActionResult Delete(int id)
		{
			var review = _db.Reviews.FirstOrDefault(r => r.IdReview == id);
			if (review == null) return NotFound();
			_db.Reviews.Remove(review);
			_db.SaveChanges();
			return RedirectToAction();
		}

		//public IActionResult List()
		//{
		//	List<Product> products = _db.Products
		//		.Include(p => p.Category)
		//		.Include(p => p.Brand)
		//		.ToList();
		//	return View(products);
		//}

		//public IActionResult Create()
		//{
		//	var model = new CreateProductModel
		//	{
		//		Brands = _db.Brands.ToList(),
		//		Categories = _db.CategoriesOfProducts.ToList()
		//	};

		//	return View(model);
		//}

		//[HttpPost]
		//public IActionResult Create(CreateProductModel model)
		//{
		//	var product = (Product)model;

		//	_db.Products.Add(product);
		//	_db.SaveChanges();

		//	int createdProductId = _db.Products.Last().IdProduct;
		//	return RedirectToAction("Edit", "Product", new { id = createdProductId });
		//}

		//public IActionResult Edit(int id)
		//{
		//	var product = _db.Products
		//		.Include(p => p.Category)
		//		.Include(p => p.Brand)
		//		.FirstOrDefault(p => p.IdProduct == id);
		//	if (product == null) return NotFound();

		//	var model = (EditProductModel)product;
		//	model.Brands = _db.Brands.ToList();
		//	model.Categories = _db.CategoriesOfProducts.ToList();

		//	return View(model);
		//}

		//[HttpPost]
		//public IActionResult Edit(EditProductModel model)
		//{
		//	var oldProduct = _db.Products.FirstOrDefault(p => p.IdProduct == model.IdProduct);
		//	if (oldProduct == null) return NotFound();

		//	oldProduct.Name = model.Name;
		//	oldProduct.Photo = model.Photo;
		//	oldProduct.Description = model.Description;
		//	oldProduct.Price = model.Price;
		//	oldProduct.Volume = model.Volume;
		//	oldProduct.CategoryId = model.CategoryId;
		//	oldProduct.BrandId = model.BrandId;

		//	_db.SaveChanges();
		//	return RedirectToAction("Edit", "Product", new { id = model.IdProduct });
		//}

		//public IActionResult Details(int id)
		//{
		//	var product = _db.Products
		//		.Include(p => p.Category)
		//		.Include(p => p.Brand)
		//		.Include(p => p.StoresProducts)
		//			.ThenInclude(sp => sp.Store)
		//				.ThenInclude(s => s.Address)
		//		.Include(p => p.Reviews)
		//			.ThenInclude(r => r.User)
		//		.FirstOrDefault(p => p.IdProduct == id);
		//	if (product == null) return NotFound();

		//	var user = HttpContext.Session.Get<User>("user");
		//	var model = new ProductDetailsModel(product, user);

		//	return View(model);
		//}

		//public IActionResult Detele(int id)
		//{
		//	var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
		//	if (product == null) return NotFound();
		//	return View(product);
		//}

		//[HttpPost]
		//public IActionResult Delete(int id)
		//{
		//	var product = _db.Products.FirstOrDefault(p => p.IdProduct == id);
		//	if (product == null) return NotFound();

		//	_db.Products.Remove(product);
		//	_db.SaveChanges();
		//	return RedirectToAction("List", "Product");
		//}
	}
}
