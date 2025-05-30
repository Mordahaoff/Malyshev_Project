using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Malyshev_Project.Models;

namespace Malyshev_Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

	public IActionResult Contacts()
	{
		return View();
	}

	public IActionResult NutritionGuide()
	{
		return View();
	}

	public IActionResult Portfolio()
	{
		return View();
	}

	public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
