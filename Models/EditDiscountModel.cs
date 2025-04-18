namespace Malyshev_Project.Models
{
	public class EditDiscountModel
	{
		public int IdDiscount { get; set; }
		public int ProductId { get; set; }
		public short Amount { get; set; }
		public DateOnly DateOfStart { get; set; }
		public DateOnly DateOfEnd { get; set; }

		// Список продуктов для демонстрации пользователю
		public List<Product> Products = [];

		public static explicit operator EditDiscountModel(Discount discount)
		{
			var model = new EditDiscountModel
			{
				ProductId = discount.ProductId,
				Amount = discount.Amount,
				DateOfStart = discount.DateOfStart,
				DateOfEnd = discount.DateOfEnd
			};
			return model;
		}
	}
}
