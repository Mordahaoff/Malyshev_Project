namespace Malyshev_Project.Models
{
	public class ProductDetailsModel
	{
		public Product Product { get; set; } = null!;
		public Review? Review { get; set; }
	}
}
