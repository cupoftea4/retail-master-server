using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class InvoiceProduct
{
    public int InvoiceProductId { get; set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int RetailPrice { get; set; }

    public int WholeReceiptProductPrice { get; set; }

    public bool? WrittenOff { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
