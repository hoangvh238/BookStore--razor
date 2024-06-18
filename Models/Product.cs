using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public Guid SupplierId { get; set; }

    public Guid CategoryId { get; set; }

    public short QuantityPerUnit { get; set; }

    public decimal UnitPrice { get; set; }

    public string ProductImage { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
