namespace Malyshev_Project.Models
{
	public class ProductCatalog(Product product)
	{
		public string Name = product.Name;
		public string Photo = product.Photo;
		public decimal Volume = product.Volume;
		public decimal Price = product.Price;
		public string Units = product.CategoryId switch
		{
			1 => "шт.",
			<= 5 => "г.",
			6 => "мл.",
			_ => throw new Exception("ProductCatalogException: Get out of range a switch.")
		};
	}
}
