using System;
using System.Collections.Generic;

namespace RetailMaster.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; } = new List<InvoiceProduct>();

    public virtual ICollection<ReceiptProduct> ReceiptProducts { get; } = new List<ReceiptProduct>();
}
