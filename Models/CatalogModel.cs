namespace Malyshev_Project.Models
{
	public class CatalogModel(List<Product> products)
	{
		public List<ProductCatalog> Products { get; set; } = 
			products.Select(p => new ProductCatalog(p)).ToList();
	}
}
