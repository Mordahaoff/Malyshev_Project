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

		// Просмотр корзины авторизованного пользователя
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
				Stores = _db.Stores.Include(s => s.Address).ToList(),
			};

			if (order != null) model.Products = order.OrdersProducts.ToList();

			return View(model);
		}

		// Оформление заказа c возможностью удаления товара из корзины
		[HttpPost]
		public IActionResult Cart(CartModel model)
		{
			if (model.Order?.StoreId == null) return BadRequest("Order is null or Chosen Store is null.");

			model.Order.StateOfOrderId = 2;
			model.Order.DateOfStatusChange = DateTime.Now;

			var store = _db.Stores
				.Include(s => s.StoresProducts)
				.First(s => s.IdStore == model.Order.StoreId);

			foreach (var productCart in model.Products!)
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
				_db.OrdersProducts.Update(productCart);
			}

			_db.Orders.Update(model.Order);
			// _db.OrdersProducts.Update(model.Order.OrdersProducts);
			_db.SaveChanges();

			return RedirectToAction("Profile", "User");
		}

		// Добавление товара из каталога товаров или карточки товара
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

		// Удаление товара из корзины
		public IActionResult RemoveProductFromCart(int id)
		{
			if (!_db.Products.Any(p => p.IdProduct == id)) return NotFound($"Product ID:[{id}] is not found. Your cart has not changed.");

			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return RedirectToAction("Login", "Auth");

			// order = cart в текущем контексте
			var order = _db.Orders
				.Include(o => o.OrdersProducts)
				.Where(o => o.StateOfOrderId == 1)
				.FirstOrDefault(o => o.UserId == user.IdUser);
			if (order == null) return NotFound("Your cart is not found.");

			var op = order.OrdersProducts.FirstOrDefault(op => op.ProductId == id);
			if (op == null) return NotFound("Your product is not in your cart.");

			_db.OrdersProducts.Remove(op);
			_db.SaveChanges();

			return RedirectToAction("Cart", "Order");
		}

		// Для изменения статуса "добавить в корзину" и "товар добавлен в корзину"
		// Проверка наличия товара в корзине (для состояния кнопки)
		[HttpGet]
		public async Task<IActionResult> IsProductInCart(int productId)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return Json(false);

			var order = await _db.Orders
				.Include(o => o.OrdersProducts)
				.FirstOrDefaultAsync(o => o.UserId == user.IdUser && o.StateOfOrderId == 1);

			return Json(order?.OrdersProducts.Any(op => op.ProductId == productId) ?? false);
		}

		// AJAX версия добавления в корзину
		[HttpPost]
		public async Task<IActionResult> AddProductToCartAjax(int productId)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return Unauthorized("Пользователь не авторизован");

			var product = await _db.Products.FindAsync(productId);
			if (product == null) return NotFound("Товар не найден");

			var order = await _db.Orders
				.Include(o => o.OrdersProducts)
				.FirstOrDefaultAsync(o => o.UserId == user.IdUser && o.StateOfOrderId == 1);

			if (order == null)
			{
				order = new Order { UserId = user.IdUser, StateOfOrderId = 1 };
				_db.Orders.Add(order);
				await _db.SaveChangesAsync();
			}

			var existingItem = order.OrdersProducts.FirstOrDefault(op => op.ProductId == productId);
			if (existingItem != null)
			{
				existingItem.CountOfProduct += 1;
				_db.OrdersProducts.Update(existingItem);
			}
			else
			{
				_db.OrdersProducts.Add(new OrdersProduct
				{
					OrderId = order.IdOrder,
					ProductId = productId,
					CountOfProduct = 1
				});
			}

			await _db.SaveChangesAsync();
			return Ok();
		}

		// AJAX версия удаления из корзины
		[HttpPost]
		public async Task<IActionResult> RemoveProductFromCartAjax(int productId)
		{
			var user = HttpContext.Session.Get<User>("user");
			if (user == null) return Unauthorized("Пользователь не авторизован");

			var order = await _db.Orders
				.Include(o => o.OrdersProducts)
				.FirstOrDefaultAsync(o => o.UserId == user.IdUser && o.StateOfOrderId == 1);

			if (order == null) return NotFound("Корзина не найдена");

			var item = order.OrdersProducts.FirstOrDefault(op => op.ProductId == productId);
			if (item == null) return NotFound("Товар не найден в корзине");

			_db.OrdersProducts.Remove(item);
			await _db.SaveChangesAsync();
			return Ok();
		}



	}
}
