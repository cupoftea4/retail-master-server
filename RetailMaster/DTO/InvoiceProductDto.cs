namespace RetailMaster.DTO;

public class CreateInvoiceProductDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int RetailPrice { get; set; }
    public int WholeReceiptProductPrice { get; set; }
    // Include any other fields that are necessary for creating an InvoiceProduct
}

public class InvoiceProductDto
{
    public int InvoiceProductId { get; set; }
    public int InvoiceId { get; set; }
    public int Quantity { get; set; }
    public int RetailPrice { get; set; }
    public int WholeReceiptProductPrice { get; set; }
    public bool WrittenOff { get; set; }
    public required ProductDto Product { get; set; }
}


public class UpdateInvoiceProductDto
{
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
    public int? RetailPrice { get; set; }
    public int? WholeReceiptProductPrice { get; set; }
}