using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malyshev_Project.Models
{
	public class EditProductModel
	{
		public int IdProduct { get; set; }
		public string Name { get; set; } = null!;
		public string Photo { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public int Volume { get; set; }

		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		//public Brand? Brand { get; set; }
		//public CategoriesOfProduct? Category { get; set; }

		public List<Brand> Brands { get; set; } = [];
		public List<CategoriesOfProduct> Categories { get; set; } = [];

		public static explicit operator EditProductModel(Product product)
		{
			var model = new EditProductModel
			{
				IdProduct = product.IdProduct,
				Name = product.Name,
				Photo = product.Photo,
				Description = product.Description,
				Price = product.Price,
				Volume = product.Volume,
				BrandId = product.BrandId,
				CategoryId = product.CategoryId
			};
			return model;
		}
	}
}
