using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Malyshev_Project.Models;

namespace Malyshev_Project.Controllers
{
    public class OrderController : Controller
    {
		private readonly ILogger<OrderController> _logger;
		private readonly PostgresContext _db;

		public OrderController(ILogger<OrderController> logger, PostgresContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var order = _db.Orders
                .Include(o => o.User)
                .Include(o => o.Store)
                    .ThenInclude(s => s.Address)
                .Include(o => o.StateOfOrder)
                .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefault(o => o.IdOrder == id);

            if (order == null) return NotFound();

            return View(order);
        }

        public IActionResult Confirm(int id)
        {
            var order = _db.Orders.FirstOrDefault(o => o.IdOrder == id);
            if (order == null) return NotFound();

            order.StateOfOrderId = 2;
            _db.SaveChanges();
            return RedirectToAction("Details", "Order", new { id = order.IdOrder });
        }

        public IActionResult Edit(int id)
        {
			var order = _db.Orders
				.Include(o => o.User)
				.Include(o => o.Store)
					.ThenInclude(s => s.Address)
				.Include(o => o.StateOfOrder)
				.Include(o => o.OrdersProducts)
					.ThenInclude(op => op.Product)
				.FirstOrDefault(o => o.IdOrder == id);

			if (order == null) return NotFound();

			return View(order);
		}

        [HttpPost]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
