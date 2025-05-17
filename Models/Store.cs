using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Store
{
    public int IdStore { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoresProduct> StoresProducts { get; set; } = new List<StoresProduct>();
}
