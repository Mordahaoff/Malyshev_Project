using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Malyshev_Project.Models
{
	public class CreateProductModel()
	{
		public string Name { get; set; } = null!;
		public string Photo { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public decimal Volume { get; set; }
		public int CategoryId { get; set; }
		public int BrandId { get; set; }

		public Brand? Brand { get; set; }
		public CategoriesOfProduct? Category { get; set; }

		public List<SelectListItem> Brands { get; set; } = [];
		public List<SelectListItem> Categories { get; set; } = [];

		public static explicit operator Product(CreateProductModel model)
		{
			var product = new Product
			{
				Name = model.Name,
				Photo = model.Photo,
				Description = model.Description,
				Price = model.Price,
				Volume = model.Volume,
				BrandId = model.BrandId,
				CategoryId = model.CategoryId
			};
			return product;
		}
		public static explicit operator CreateProductModel(Product product) {
			var model = new CreateProductModel
			{
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
