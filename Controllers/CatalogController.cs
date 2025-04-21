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

	public IActionResult Products(int? categoryId, int? brandId, int? sortId)
	{
		List<Product> products = _db.Products.ToList();
		var model = new CatalogModel();

		if (categoryId != null)
		{
			products = products.Where(p => p.CategoryId == categoryId).ToList();
			model.CategoryName = _db.CategoriesOfProducts.FirstOrDefault(c => c.IdCategory == categoryId)?.Name;
			model.CategoryId = categoryId;
			if (model.CategoryName == null) return BadRequest();
		}
		if (brandId != null)
		{
			products = products.Where(p => p.BrandId == brandId).ToList();
			model.BrandName = _db.Brands.FirstOrDefault(b => b.IdBrand == brandId)?.Name;
			model.BrandId = brandId;
			if (model.BrandName == null) return BadRequest();
		}
		if (sortId != null)
		{
			switch (sortId)
			{
				case 1: // �� ����������� ����
					products = products.OrderBy(p => p.Price).ToList();
					model.SortName = "�� ����������� ����";
					break;
				case 2: // �� �������� ����
					products = products.OrderByDescending(p => p.Price).ToList();
					model.SortName = "�� �������� ����";
					break;
				case 3: // ������ �� �������
					products = _db.Products
						.Include(p => p.Discounts)
						.Where(p => p.Discounts.Any(d => d.DateOfEnd < DateOnly.FromDateTime(DateTime.Now)))
						.ToList();
					model.SortName = "������ �� �������";
					//products = products.Where(p => p.Discounts.Any(d => d.DateOfEnd < DateOnly.FromDateTime(DateTime.Now))).ToList();
					break;
				default:
					return BadRequest();
			}
			model.SortId = sortId;
		}

		model.Products = products.Select(p => new ProductCatalog(p)).ToList();
		_logger.LogInformation($"���-�� ���������: {products.Count}");
		return View(model);
	}

	public IActionResult AddProductToOrder(int id)
	{
		var user = HttpContext.Session.Get<User>("user");
		if (user == null) return RedirectToAction("Login", "Auth");

		var order = _db.Orders
			.Include(o => o.OrdersProducts)
			.FirstOrDefault(o => o.UserId == user.IdUser);

		if (_db.Products.FirstOrDefault(p => p.IdProduct == id) == null) return BadRequest();

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

		_logger.LogInformation($"����� ID: [{id}] �������� � ������� ID: [{order.IdOrder}] ������������ ID: [{user.IdUser}]");
		return RedirectToAction("Products", "Catalog");
	}
}
