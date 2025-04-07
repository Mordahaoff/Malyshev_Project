namespace Malyshev_Project.Models
{
	public static class ProductExtension
	{
		public static void UpdateFrom(this Product oldProduct, ProductModel editedProduct)
		{
			oldProduct.Name = editedProduct.Name;
			oldProduct.Photo = editedProduct.Photo;
			oldProduct.Description = editedProduct.Description;
			oldProduct.Price = editedProduct.Price;
			oldProduct.Volume = editedProduct.Volume;
			oldProduct.CategoryId = editedProduct.CategoryId;
			oldProduct.BrandId = editedProduct.BrandId;
		}
	}
}
