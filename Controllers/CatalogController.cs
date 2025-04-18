using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project.Controllers;

public class CatalogController : Controller
{
	private readonly ILogger<CatalogController> _logger;
	private readonly PostgresContext _db;

	public CatalogController(ILogger<CatalogController> logger, PostgresContext db)
	{
		_logger = logger;
		_db = db;
	}

	// Разработать функционал для поиска продуктов согласно каким-либо критериям.
	public IActionResult Products(int? categoryId, int? brandId)
	{
		List<Product> products = _db.Products.ToList();
		_logger.LogInformation($"Кол-во продуктов: {products.Count}");
		var model = new CatalogModel(products);
		return View(model);
	}

	public IActionResult AddProductToOrder(int id)
	{
		var user = HttpContext.Session.Get<User>("user");
		if (user == null) return RedirectToAction("Login", "Auth");

		var order = _db.Orders
			.Include(o => o.OrdersProducts)
			.FirstOrDefault(o => o.UserId == user.IdUser);

		if (order == null)
		{
			_db.Orders.Add(new Order { UserId = user.IdUser });
			_db.SaveChanges();
			order = _db.Orders.Last();
		}

		if (order.OrdersProducts.Any(op => op.ProductId == id))
		{
			order.OrdersProducts.First(op => op.ProductId == id).CountOfProduct += 1;
		}
		else
		{
			_db.OrdersProducts.Add(new OrdersProduct { OrderId = order.IdOrder, ProductId = id, CountOfProduct = 1 });
			_db.SaveChanges();
		}

		return RedirectToAction("Products", "Catalog");
	}
}
