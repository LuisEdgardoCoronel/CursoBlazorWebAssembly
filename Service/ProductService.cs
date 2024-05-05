using CursoBlazorWebAssembly.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CursoBlazorWebAssembly.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _serializerOptions;

        public ProductService(HttpClient httpClient, JsonSerializerOptions options)
        {
            _httpClient = httpClient;
            _serializerOptions = options;
        }


        public async Task<List<Product>?> GetProducts()
        {
            var response = await _httpClient.GetAsync("/v1/products");

            return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
        }


        public async Task AddProducts(Product product)
        {
            var response = await _httpClient.PostAsync($"v1/products", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }


        public async Task UpdateProduct(int productID, Product product)
        {
            var response = await _httpClient.PutAsync($"v1/products/{productID}", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DeleteProducts(int productID)
        {
            var response = await _httpClient.DeleteAsync($"v1/products/{productID}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

    }
}
