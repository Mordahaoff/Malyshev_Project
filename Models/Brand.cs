using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Brand
{
    public int IdBrand { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
