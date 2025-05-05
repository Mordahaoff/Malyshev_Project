namespace Malyshev_Project.Models
{
	public class EditOrderModel
	{
		public Order Order { get; set; }
		public List<StatesOfOrder> StatesOfOrder { get; set; } = [];
		public List<Store> Stores { get; set; } = [];
	}
}
