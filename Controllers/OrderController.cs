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

		//public IActionResult Confirm(int id)
		//{
		//	var order = _db.Orders.FirstOrDefault(o => o.IdOrder == id);
		//	if (order == null) return NotFound();

		//	order.StateOfOrderId = 2;
		//	_db.SaveChanges();
		//	return RedirectToAction("Details", "Order", new { id });
		//}

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

			model.StatesOfOrder = _db.StatesOfOrders.ToList();
			model.Stores = _db.Stores.Include(s => s.Address).ToList();

			var allProducts = _db.Products
				.ToList();

			foreach (var product in allProducts)
			{
				model.ProductsInOrder.Add(new ProductOrder { Product = product, CountOfProduct = _db.OrdersProducts.Where(op => op.ProductId == product.IdProduct).FirstOrDefault(op => op.OrderId == id)?.CountOfProduct ?? 0 });
			}

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditOrderModel model)
		{
			var oldOrder = _db.Orders.FirstOrDefault(o => o.IdOrder == model.IdOrder);
			if (oldOrder == null) return NotFound();

			oldOrder.StateOfOrderId = model.StateOfOrderId;
			oldOrder.StoreId = model.StoreId;
			oldOrder.DateOfStatusChange = model.DateOfStatusChange;

			foreach (var productOrder in model.ProductsInOrder)
			{
				var oldOp = _db.OrdersProducts.Where(op => op.ProductId == productOrder.Product.IdProduct).FirstOrDefault(op => op.OrderId == model.IdOrder);
				if (productOrder.CountOfProduct <= 0 && oldOp != null)
				{
					_db.OrdersProducts.Remove(oldOp);
				}
				else if (productOrder.CountOfProduct > 0)
				{
					if (oldOp == null)
					{
						_db.OrdersProducts.Add(new OrdersProduct { OrderId = model.IdOrder, ProductId = productOrder.Product.IdProduct, CountOfProduct = productOrder.CountOfProduct });
					}
					else
					{
						oldOp.CountOfProduct = productOrder.CountOfProduct;
					}
				}
			}

			_db.SaveChanges();
			return RedirectToAction("Edit", "Order", new { id = model.IdOrder });
		}

		public IActionResult Cart()
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");

			var order = _db.Orders
				.Include(o => o.OrdersProducts)
					.ThenInclude(op => op.Product)
				.FirstOrDefault(o => o.UserId == user.IdUser && o.StateOfOrderId == 1);

			var model = new CartModel()
			{
				Order = order,
				Stores = _db.Stores.Include(s => s.Address).ToList()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Cart(CartModel model)
		{
			if (model.Order == null || model?.StoreId == null) return BadRequest();

			model.Order.StateOfOrderId = 2;
			model.Order.StoreId = model.StoreId;

			var store = _db.Stores
				.Include(s => s.StoresProducts)
				.First(s => s.IdStore == model.StoreId);

			foreach (var productCart in model.Order.OrdersProducts)
			{
				var productStore = store.StoresProducts.First(sp => sp.ProductId == productCart.ProductId);
				var difference = productCart.CountOfProduct - productStore.CountOfProduct;
				if (difference <= 0) // Если клиент заказал не более, чем есть в магазине
				{
					// Снимаем с магазина столько, сколько заказал пользователь
					productStore.CountOfProduct -= productCart.CountOfProduct;
				}
				else // Если клиент заказал больше, чем есть в магазине
				{
					// Производим доставку 10 продуктов в магазин и отдаем клиенту столько, сколько он заказал
					productStore.CountOfProduct += (short)(10 - Math.Abs(difference));
				}
				_db.StoresProducts.Update(productStore);
			}

			_db.Orders.Update(model.Order);
			_db.SaveChanges();

			return RedirectToAction("Details", "Order", new { id = model.Order.IdOrder });
		}

		public IActionResult AddProductToCart(int id)
		{
			if (_db.Products.FirstOrDefault(p => p.IdProduct == id) == null) return BadRequest();

			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");

			// order = cart в текущем контексте
			var order = _db.Orders
				.Include(o => o.OrdersProducts)
				.Where(o => o.StateOfOrderId == 1)
				.FirstOrDefault(o => o.UserId == user.IdUser);

			if (order == null)
			{
				_db.Orders.Add(new Order { UserId = user.IdUser });
				_db.SaveChanges();
				order = _db.Orders.OrderBy(o => o.IdOrder).Last();
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

			_logger.LogInformation($"Товар ID: [{id}] добавлен в корзину ID: [{order.IdOrder}] пользователя ID: [{user.IdUser}]");

			// Получение URL страницы, с которой пришёл запрос
			var referer = HttpContext.Request.Headers.Referer.ToString();

			if (!string.IsNullOrEmpty(referer))
			{
				return Redirect(referer);
			}
			else
			{
				// по умолчанию — редирект на страницу каталога или другую страницу
				return RedirectToAction("Products", "Catalog");
			}
		}

		public IActionResult EditCountOfProductInCart()
		{
			return RedirectToAction("Profile", "User");
		}

		public IActionResult RemoveProductFromCart(int id)
		{
			if (_db.Products.FirstOrDefault(p => p.IdProduct == id) == null) return BadRequest();

			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");

			// order = cart в текущем контексте
			var order = _db.Orders
				.Include(o => o.OrdersProducts)
				.Where(o => o.StateOfOrderId == 1)
				.First(o => o.UserId == user.IdUser);

			var op = order.OrdersProducts.FirstOrDefault(op => op.ProductId == id);
			if (op == null) return BadRequest();

			order.OrdersProducts.Remove(op);
			_db.SaveChanges();

			return RedirectToAction("Cart", "Order");
		}
	}
}
