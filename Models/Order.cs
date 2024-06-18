using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateOnly RequiredDate { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly ShippedDate { get; set; }

    public decimal? Freight { get; set; }

    public string ShipAddress { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
