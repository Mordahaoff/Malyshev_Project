using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malyshev_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly ILogger<OrderController> _logger;
		private readonly PostgresContext _db;

		public OrderController(ILogger<OrderController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult List(int? storeId)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var orders = _db.Orders
				.Include(o => o.StateOfOrder)
				.Include(o => o.Store)
					.ThenInclude(s => s.Address)
				.Include(o => o.OrdersProducts)
					.ThenInclude(op => op.Product)
				.Include(o => o.User)
				.Where(o => o.StateOfOrderId != 1)
				.OrderBy(o => o.StateOfOrderId)
				.ToList();

			if (storeId != null)
			{
				orders = orders.Where(o => o.StoreId == storeId).ToList();
			}

			return View(orders);
		}

		public IActionResult Details(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var order = _db.Orders
				.Include(o => o.User)
				.Include(o => o.Store)
					.ThenInclude(s => s.Address)
				.Include(o => o.StateOfOrder)
				.Include(o => o.OrdersProducts)
					.ThenInclude(op => op.Product)
				.FirstOrDefault(o => o.IdOrder == id);
			if (order == null) return NotFound($"Order ID:[{id}] is not found.");

			return View(order);
		}

		public IActionResult Edit(int id)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			var order = _db.Orders
				.Include(o => o.User)
				.Include(o => o.Store)
					.ThenInclude(s => s.Address)
				.Include(o => o.StateOfOrder)
				.Include(o => o.OrdersProducts)
					.ThenInclude(op => op.Product)
				.FirstOrDefault(o => o.IdOrder == id);
			if (order == null) return NotFound($"Order ID:[{id}] is not found.");
			
			//отображение всех статусов
			ViewBag.States = new SelectList(_db.StatesOfOrders, "IdState", "Name", order.StateOfOrderId);

			return View(order);
		}

		// Администратор может поменять только статус заказа
		[HttpPost]
		public IActionResult Edit(Order order)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user?.RoleId != 2) return BadRequest("You are not an admin.");

			_db.Orders.Update(order);
			_db.SaveChanges();
			return RedirectToAction("Details", "Order", new { id = order.IdOrder });
		}
	}
}
