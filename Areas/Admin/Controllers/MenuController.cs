using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MenuController : Controller
	{
		private readonly ILogger<MenuController> _logger;
		private readonly PostgresContext _db;

		public MenuController(ILogger<MenuController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		[Route("{area}")]
		[Route("{area}/{controller}")]
		[Route("{area}/{controller}/{action}")]
		public IActionResult Index()
		{
			// var user = HttpContext.Session.Get<User>("user");
			// if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			return View();
		}
	}
}
