using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Malyshev_Project.Models
{
	public class CreateProductModel()
	{
		public Product Product { get; set; } = new Product();
		public List<Brand> Brands { get; set; } = [];
		public List<CategoriesOfProduct> Categories { get; set; } = [];
	}
}
