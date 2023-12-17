using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfClient.Models;
using WpfClient.Services;

namespace WpfClient.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _productCategoryService;
        private ObservableCollection<Product> _products;
        private ObservableCollection<ProductCategory> _categories;
        public ObservableCollection<ProductCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        private int _selectedCategoryId;
        public int SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                OnPropertyChanged(nameof(SelectedCategoryId));
            }
        }
        
        private Product _selectedProduct;
        
        public Product TestProduct { get; set; } = new Product
        {
            Name = "Test Product",
            Barcode = "1234567890",
            Category = new ProductCategory
            {
                Name = "Test Category"
            }
        };
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        
        public ProductViewModel(ProductService productService, ProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            LoadProducts();
            LoadCategories();
            UpdateProductCommand = new RelayCommand(param => UpdateProduct(param as Product));
            DeleteProductCommand = new RelayCommand(param => DeleteProduct((int)param));
        }
        
        private async void LoadCategories()
        {
            // Assuming you have a method to fetch categories
            var categories = await _productCategoryService.GetCategoriesAsync();
            categories?.ForEach(Console.WriteLine);
            Categories = new ObservableCollection<ProductCategory>(categories ?? throw new InvalidOperationException());
        }

        public async void LoadProducts()
        {
            Products = new ObservableCollection<Product>(await _productService.GetProductsAsync());
        }
        
        public async void UpdateProduct(Product product)
        {
            await _productService.UpdateProductAsync(product.ProductId, product);
            LoadProducts(); // Refresh the list
        }
        
        public async void DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            LoadProducts(); // Refresh the list
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}