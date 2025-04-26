using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malyshev_Project.Models
{
	public class EditProductModel()
	{
		public Product Product { get; set; } = null!;
		public List<Brand> Brands { get; set; } = [];
		public List<CategoriesOfProduct> Categories { get; set; } = [];
	}
}
