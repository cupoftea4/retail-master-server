using System.Windows;
using WpfClient.Models;
using WpfClient.Services;
using WpfClient.UserControls;
using WpfClient.ViewModels;

namespace WpfClient.Views
{
    public partial class MainWindow : Window
    {
        private readonly ProductViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ProductViewModel(
                new ProductService(App.HttpClient),
                new ProductCategoryService(App.HttpClient));
            DataContext = _viewModel;
        }
        
        private void OpenAddProductWindow_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(new ProductService(App.HttpClient));
            var result = addProductWindow.ShowDialog();
            if (result == true)
            {
                _viewModel.LoadProducts();
            }
        }
    }
}