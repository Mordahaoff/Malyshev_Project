namespace Malyshev_Project.Models
{
	public class CatalogModel()
	{
		public List<ProductUnits> Products { get; set; } = [];
		public string? CategoryName { get; set; } = "Не выбрана";
		public int? CategoryId { get; set; }
		public string? BrandName { get; set; } = "Не выбран";
		public int? BrandId { get; set; }
		public string? SortName { get; set; } = "Не выбрана";
		public int? SortId { get; set; }
	}
}
