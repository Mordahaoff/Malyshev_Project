using System;
using System.Collections.Generic;

namespace Malyshev_Project.Models;

public partial class OrdersProduct
{
    public int IdOrdersProducts { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public short CountOfProduct { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
