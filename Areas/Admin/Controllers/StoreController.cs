using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StoreController : Controller
	{
		private readonly ILogger<StoreController> _logger;
		private readonly PostgresContext _db;

		public StoreController(ILogger<StoreController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult List(string? storeAddress)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var stores = _db.Stores
				.Include(s => s.Address)
				.OrderBy(s => s.IdStore)
				.ToList();

			// Пример пользовательского ввода: "Ярославль", "Гагарина"
			if (storeAddress != null)
			{
				stores = stores.Where(s =>
					s.Address.City.ToLower().Contains(storeAddress.ToLower()) ||
					s.Address.Street.ToLower().Contains(storeAddress.ToLower()) ||
					s.Address.Building.Contains(storeAddress)).ToList();
				ViewData["Request"] = storeAddress;
			}

			return View(stores);
		}

		public IActionResult Details(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var store = _db.Stores
				.Include(s => s.Address)
				.Include(s => s.StoresProducts)
					.ThenInclude(sp => sp.Product)
				.FirstOrDefault(s => s.IdStore == id);
			if (store == null) return NotFound($"Store [ID:{id}] is not found.");

			return View(store);
		}

		public IActionResult Create()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var store = new Store();
			return View(store);
		}

		[HttpPost]
		public IActionResult Create(Store store)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			_db.Stores.Add(store);
			_db.Addresses.Add(store.Address);
			_db.SaveChanges();

			store = _db.Stores.OrderBy(s => s.IdStore).Last();

			var products = _db.Products.ToList();
			foreach (var product in products)
			{
				_db.StoresProducts.Add(new StoresProduct
				{
					StoreId = store.IdStore,
					ProductId = product.IdProduct,
					CountOfProduct = 0
				});
			}

			_db.SaveChanges();
			return RedirectToAction("Edit", "Store", new { id = store.IdStore });
		}

		public IActionResult Edit(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var store = _db.Stores
				.Include(s => s.Address)
				.Include(s => s.StoresProducts)
					.ThenInclude(sp => sp.Product)
				.FirstOrDefault(s => s.IdStore == id);
			if (store == null) return NotFound($"Store [ID:{id}] is not found.");

			return View(store);
		}

		[HttpPost]
		public IActionResult Edit(Store store)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			_db.Stores.Update(store);
			_db.Addresses.Update(store.Address);
			foreach (var sp in store.StoresProducts)
			{
				_db.StoresProducts.Update(sp);
			}
			_db.SaveChanges();

			return RedirectToAction("Edit", "Store", new { id = store.IdStore });
		}

		public IActionResult Delete(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var store = _db.Stores.FirstOrDefault(s => s.IdStore == id);
			if (store == null) return NotFound($"Store [ID:{id}] is not found.");

			_db.Stores.Remove(store);
			_db.SaveChanges();
			return RedirectToAction("List", "Store");
		}
	}
}
