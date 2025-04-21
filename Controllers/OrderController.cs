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

            var model = (EditOrderModel)order;

            model.Products = _db.Products.ToList();
            model.StatesOfOrder = _db.StatesOfOrders.ToList();
            model.Stores = _db.Stores.Include(s => s.Address).ToList();

			return View(model);
		}

        [HttpPost]
        public IActionResult Edit(EditOrderModel model)
        {
            var oldOrder = _db.Orders.FirstOrDefault(o => o.IdOrder == model.IdOrder);
            if (oldOrder == null) return NotFound();

            oldOrder.StateOfOrderId = model.StateOfOrderId;
            //oldOrder.StoreId = model.StoreId;

            return RedirectToAction("Details", "Order", new { id = model.IdOrder });
        }
    }
}
