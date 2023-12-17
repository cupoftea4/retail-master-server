using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>?> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("products");
        }
        
        public async Task AddProductAsync(CreateProductDto product)
        {
            // Add logic to POST the product to your API
            await _httpClient.PostAsJsonAsync("products", product);
        }
        
        public async Task UpdateProductAsync(int productId, Product updatedProduct)
        {
            await _httpClient.PatchAsJsonAsync($"products/{productId}", new UpdateProductDto
            {
                Name = updatedProduct.Name,
                Barcode = updatedProduct.Barcode,
                CategoryId = updatedProduct.Category.CategoryId
            });
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _httpClient.DeleteAsync($"products/{productId}");
        }

    }
}