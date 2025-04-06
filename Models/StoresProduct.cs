using System;
using System.Collections.Generic;

namespace Malyshev_Project.Models;

public partial class StoresProduct
{
    public int IdStoresProducts { get; set; }

    public int StoreId { get; set; }

    public int ProductId { get; set; }

    public short CountOfProduct { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
