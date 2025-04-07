using AspNetCoreGeneratedDocument;
using Malyshev_Project.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Malyshev_Project.Models
{
	public class ProductModel()
	{
		public int? IdProduct { get; set; }
		public string Name { get; set; } = null!;

		public string Photo { get; set; } = null!;

		public string Description { get; set; } = null!;

		public decimal Price { get; set; }

		public decimal Volume { get; set; }

		public int CategoryId { get; set; }

		public int BrandId { get; set; }

		//public virtual Brand Brand { get; set; } = null!;

		//public virtual CategoriesOfProduct Category { get; set; } = null!;

		public List<SelectListItem> Brands { get; set; } = [];
		public List<SelectListItem> Categories { get; set; } = [];

		// Явное

		public static explicit operator Product(ProductModel model)
		{
			var product = new ProductBuilder()
				.WithName(model.Name)
				.WithPhoto(model.Photo)
				.WithDescription(model.Description)
				.WithPrice(model.Price)
				.WithVolume(model.Volume)
				.WithBrandId(model.BrandId)
				.WithCategoryId(model.CategoryId)
				.Build();
			return product;
		}

		// Явное
		public static explicit operator ProductModel(Product product) {
			var model = new ProductModelBuilder()
				.WithName(product.Name)
				.WithPhoto(product.Photo)
				.WithDescription(product.Description)
				.WithPrice(product.Price)
				.WithVolume(product.Volume)
				.WithBrandId(product.BrandId)
				.WithCategoryId(product.CategoryId)
				.Build();
			return model;
		}
	}
}
