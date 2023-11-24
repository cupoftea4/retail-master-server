using RetailMaster.Models;

namespace RetailMaster.DTO;

public class CreateInvoiceDto
{
    public int ShopId { get; set; }
    public string? Note { get; set; }
}

public class UpdateInvoiceDto
{
    public string? Note { get; set; }
    public bool? Printed { get; set; }
    public int? ShopId { get; set; }
}

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public required string Date { get; set; }
    public string? Note { get; set; }
    public bool? Printed { get; set; }
    public required Shop Shop { get; set; }
}