namespace Malyshev_Project.Models
{
	public class CartModel
	{
		public Order? Order { get; set; } = null!;
		public int StoreId { get; set; } // Выбранный пользователем магазин для доставки
		public IEnumerable<Store> Stores { get; set; } = null!;
	}
}
