using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Account
{
    public Guid AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Role { get; set; } = null!;
}
