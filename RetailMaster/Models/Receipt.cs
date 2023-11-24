using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public int ClientId { get; set; }

    public int ShopId { get; set; }

    public int SellerId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<ReceiptProduct> ReceiptProducts { get; } = new List<ReceiptProduct>();

    public virtual User Seller { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
