namespace WpfClient.Models;

public class ProductCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"CategoryId: {CategoryId}, Name: {Name}";
    }
}