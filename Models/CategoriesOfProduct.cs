using System;
using System.Collections.Generic;

namespace Malyshev_Project.Models;

public partial class CategoriesOfProduct
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
