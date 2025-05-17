using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Order
{
    public int IdOrder { get; set; }

    public int StateOfOrderId { get; set; }

    public int? StoreId { get; set; }

    public int UserId { get; set; }

    public DateTime DateOfStatusChange { get; set; }

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual StatesOfOrder StateOfOrder { get; set; } = null!;

    public virtual Store? Store { get; set; }

    public virtual User User { get; set; } = null!;
}
