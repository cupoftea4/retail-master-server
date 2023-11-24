using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;
}
