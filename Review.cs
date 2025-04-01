using System;
using System.Collections.Generic;

namespace Malyshev_Project;

public partial class Review
{
    public int IdReview { get; set; }

    public string Text { get; set; } = null!;

    public short Grade { get; set; }

    public DateOnly DateOfCreation { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
