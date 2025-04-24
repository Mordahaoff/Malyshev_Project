namespace Malyshev_Project.Models
{
	public class ProductDetailsModel(Product product, User user)
	{
		public Product Product { get; set; } = product;
		public string Units { get; set; } = product.CategoryId switch
		{
			1 => "шт.",
			<= 5 => "г.",
			6 => "мл.",
			_ => throw new Exception("ProductCatalogException: Get out of range a switch.")
		};
		// Авторизованный пользователь?
		public User? User { get; set; } = user;
		public Review? Review { get; set; }
	}
}
