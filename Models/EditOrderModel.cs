namespace Malyshev_Project.Models
{
	public class EditOrderModel
	{
		public int IdOrder { get; set; }
		public int StateOfOrderId { get; set; }
		public int? StoreId { get; set; }
		public Store? Store { get; set; }
		public User User { get; set; } = null!;
		public DateTime DateOfStatusChange { get; set; }
		public List<ProductOrder> ProductsInOrder { get; set; } = [];
		//public List<OrdersProduct> OrdersProducts { get; set; } = [];
		//public List<Product> Products { get; set; } = [];
		public List<StatesOfOrder> StatesOfOrder { get; set; } = [];
		public List<Store> Stores { get; set; } = [];

		public static explicit operator EditOrderModel(Order order)
		{
			var model = new EditOrderModel
			{
				IdOrder = order.IdOrder,
				StateOfOrderId = order.StateOfOrderId,
				Store = order.Store,
				User = order.User,
				DateOfStatusChange = order.DateOfStatusChange,
				//OrdersProducts = order.OrdersProducts.ToList()
			};
			return model;
		}
	}
}
