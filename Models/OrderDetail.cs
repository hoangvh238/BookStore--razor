using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class OrderDetail
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
