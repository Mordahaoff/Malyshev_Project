using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int Volume { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual CategoriesOfProduct Category { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<StoresProduct> StoresProducts { get; set; } = new List<StoresProduct>();

    public string GetUnits()
    {
		return CategoryId switch
		{
			1 => "шт.",
			<= 5 => "г.",
			6 => "мл.",
			_ => throw new Exception("GetUnits Exception: Get out of range a switch.")
		};
	}
}
