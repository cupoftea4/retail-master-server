using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class PersonalDiscount
{
    public int DiscountId { get; set; }

    public int ClientId { get; set; }

    public double DiscountRate { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<ReceiptProduct> ReceiptProducts { get; } = new List<ReceiptProduct>();
}
