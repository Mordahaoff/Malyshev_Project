namespace Malyshev_Project.Models
{
	public class CartModel
	{
		public Order? Order { get; set; }
		public IEnumerable<Store> Stores { get; set; } = null!;
		public List<OrdersProduct>? Products { get; set; }
	}
}
