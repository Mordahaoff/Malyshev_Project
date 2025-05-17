using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Discount
{
    public int IdDiscount { get; set; }

    public int ProductId { get; set; }

    public short Amount { get; set; }

    public DateOnly DateOfStart { get; set; }

    public DateOnly DateOfEnd { get; set; }

    public virtual Product Product { get; set; } = null!;
}
