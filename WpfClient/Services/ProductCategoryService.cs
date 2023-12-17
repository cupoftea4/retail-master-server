using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.Services;

public class ProductCategoryService
{
    private readonly HttpClient _httpClient;

    public ProductCategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductCategory>?> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductCategory>>("product-categories");
    }
}
