namespace Malyshev_Project.Models
{
	public class ProductDetailsModel(Product product, User user)
	{
		public ProductUnits ProductUnits { get; set; } = new ProductUnits(product);
		public User? User { get; set; } = user;	// Авторизованный пользователь?
		public Review? Review { get; set; }	// Объект отзыва для авторизованного пользователя?
	}
}
