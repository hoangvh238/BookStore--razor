using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string Password { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string? Adress { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
