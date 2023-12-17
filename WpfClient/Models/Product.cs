using System.ComponentModel;

namespace WpfClient.Models;
// public class Product
// {
//     public int ProductId { get; set; }
//     public required ProductCategory Category { get; set; }
//     public string Name { get; set; } = null!;
//     public string Barcode { get; set; } = null!;
// }

public class Product : INotifyPropertyChanged
{
    public int ProductId { get; set; }
    public required ProductCategory Category { get; set; }
    public string Barcode { get; set; } = null!;
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


public class CreateProductDto
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public required string Barcode { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Barcode: {Barcode}, CategoryId: {CategoryId}";
    }
}

public class UpdateProductDto
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public required string Barcode { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Barcode: {Barcode}, CategoryId: {CategoryId}";
    }
}

