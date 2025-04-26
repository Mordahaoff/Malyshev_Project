namespace Malyshev_Project.Models
{
	public class ProductCatalog(Product product)
	{
		public Product Product { get; set; } = product;
		public string Units = product.CategoryId switch
		{
			1 => "шт.",
			<= 5 => "г.",
			6 => "мл.",
			_ => throw new Exception("ProductCatalogException: Get out of range a switch.")
		};

		//public int IdProduct = product.IdProduct;
		//public string Name = product.Name;
		//public string Photo = product.Photo;
		//public decimal Volume = product.Volume;
		//public decimal Price = product.Price;
	}
}
