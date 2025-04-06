using System;
using System.Collections.Generic;

namespace Malyshev_Project.Models;

public partial class Address
{
    public int IdAddress { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
