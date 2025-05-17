namespace Malyshev_Project.Models;

public class StoreModel()
{
    public Store Store { get; set; } = null!;
    public List<StoresProduct> Products { get; set; } = null!;
}