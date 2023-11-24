using RetailMaster.Models;

namespace RetailMaster.DTO;

public class CreateProductDto
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public required string Barcode { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    public required ProductCategory Category { get; set; }
    public string Name { get; set; } = null!;
    public string Barcode { get; set; } = null!;
}

public class UpdateProductDto
{
    public int? CategoryId { get; set; }
    public string Name { get; set; } = null!;
}
