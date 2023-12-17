using System;
using WpfClient.Models;
using WpfClient.Services;
using System.Windows;
using WpfClient.ViewModels;

namespace WpfClient.Views
{
    public partial class AddProductWindow : Window
    {
        private readonly ProductService _productService;
        private readonly ProductViewModel _viewModel;

        public AddProductWindow(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            _viewModel = new ProductViewModel(new ProductService(App.HttpClient), new ProductCategoryService(App.HttpClient));
            DataContext = _viewModel;

        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var newProduct = new CreateProductDto
            {
                Name = NameTextBox.Text,
                Barcode = BarcodeTextBox.Text,
                CategoryId = _viewModel.SelectedCategoryId
                // Set other properties from input fields
            };
            
            Console.WriteLine(newProduct);

            await _productService.AddProductAsync(newProduct);
            
            DialogResult = true;
            Close();
        }
    }
}