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
		List<Product> products = _db.Products.OrderBy(p => p.IdProduct).ToList();
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
				case 1: // По возрастанию цены
					{
						products = products.OrderBy(p => p.Price).ToList();
						model.SortName = "По возрастанию цены";
						break;
					}
				case 2: // По убыванию цены
					{
						products = products.OrderByDescending(p => p.Price).ToList();
						model.SortName = "По убыванию цены";
						break;
					}
				default:
					return BadRequest();
			}
			model.SortId = sortId;
		}

		model.Products = products;
		_logger.LogInformation($"Count of product in Catalog: {products.Count}");
		return View(model);
	}
}
