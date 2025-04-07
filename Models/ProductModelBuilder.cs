namespace Malyshev_Project.Models
{
	public class ProductModelBuilder
	{
		private ProductModel _productModel = new ProductModel();

		public ProductModelBuilder WithName(string name)
		{
			_productModel.Name = name;
			return this;
		}

		public ProductModelBuilder WithPhoto(string url)
		{
			_productModel.Photo = url;
			return this;
		}

		public ProductModelBuilder WithDescription(string description)
		{
			_productModel.Description = description;
			return this;
		}

		public ProductModelBuilder WithPrice(decimal price)
		{
			_productModel.Price = price;
			return this;
		}

		public ProductModelBuilder WithVolume(decimal volume)
		{
			_productModel.Volume = volume;
			return this;
		}

		public ProductModelBuilder WithBrandId(int id)
		{
			_productModel.BrandId = id;
			return this;
		}

		public ProductModelBuilder WithCategoryId(int id)
		{
			_productModel.CategoryId = id;
			return this;
		}

		public ProductModel Build()
		{
			return _productModel;
		}
	}
}
