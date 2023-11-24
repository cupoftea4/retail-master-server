using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class ReceiptProduct
{
    public int ReceiptProductId { get; set; }

    public int ReceiptId { get; set; }

    public int ProductId { get; set; }

    public int? DiscountId { get; set; }

    public int Price { get; set; }

    public string Date { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual PersonalDiscount? Discount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
