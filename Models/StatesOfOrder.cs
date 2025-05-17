using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class StatesOfOrder
{
    public int IdState { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
