using System.Windows.Controls;
using WpfClient.Models;

namespace WpfClient.UserControls
{
    public partial class ProductDetailControl : UserControl
    {
        public ProductDetailControl()
        {
            InitializeComponent();
            DataContext = new Product
            {
                Name = "Testiiing",
                Category = new ProductCategory
                {
                    Name = "Test Category"
                },
                Barcode = "1234567890",
                ProductId = 1,
            };
        }
    }
}