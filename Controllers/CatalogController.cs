using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Products()
    {
        List<Product> products = _db.Products.ToList();
        _logger.LogInformation($"Кол-во продуктов: {products.Count}");
        var model = new CatalogModel(products);
        return View(model);
    }
}
