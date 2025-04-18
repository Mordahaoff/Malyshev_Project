namespace Malyshev_Project.Models
{
	public class CreateDiscountModel
	{
		public short Amount { get; set; }
		public DateOnly DateOfStart { get; set; }
		public DateOnly DateOfEnd { get; set; }
		public Product? Product { get; set; }

		// Список продуктов для демонстрации пользователю
		public List<Product> Products = [];

		public static explicit operator Discount(CreateDiscountModel model)
		{
			var discount = new Discount
			{
				ProductId = model.Product!.IdProduct,
				Amount = model.Amount,
				DateOfStart = model.DateOfStart,
				DateOfEnd = model.DateOfEnd,
			};
			return discount;
		}
	}
}
