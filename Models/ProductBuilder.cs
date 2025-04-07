namespace Malyshev_Project.Models
{
	public class ProductBuilder
	{
		private Product _product = new Product();

		public ProductBuilder WithName(string name)
		{
			_product.Name = name;
			return this;
		}

		public ProductBuilder WithPhoto(string url)
		{
			_product.Photo = url;
			return this;
		}

		public ProductBuilder WithDescription(string description)
		{
			_product.Description = description;
			return this;
		}

		public ProductBuilder WithPrice(decimal price)
		{
			_product.Price = price;
			return this;
		}

		public ProductBuilder WithVolume(decimal volume)
		{
			_product.Volume = volume;
			return this;
		}

		public ProductBuilder WithBrandId(int id)
		{
			_product.BrandId = id;
			return this;
		}

		public ProductBuilder WithCategoryId(int id)
		{
			_product.CategoryId = id;
			return this;
		}

		public Product Build()
		{
			return _product;
		}
	}
}
