namespace Malyshev_Project.Models
{
	public class ProductDetailsModel(Product product)
	{
		public ProductUnits ProductUnits { get; set; } = new ProductUnits(product);
		public Review? Review { get; set; }	// Объект отзыва для авторизованного пользователя?
	}
}
