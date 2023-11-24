using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }
}
