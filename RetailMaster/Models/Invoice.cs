using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int ShopId { get; set; }

    public string Date { get; set; } = null!;

    public string? Note { get; set; }

    public bool? Printed { get; set; }

    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; } = new List<InvoiceProduct>();

    public virtual Shop Shop { get; set; } = null!;
}
