﻿using CursoBlazorWebAssembly.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CursoBlazorWebAssembly.Service
{
    public class ProductService:IProductService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _serializerOptions;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task<List<Product>?> GetProducts()
        {
            var response = await _httpClient.GetAsync("api/v1/products");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions);
        }


        public async Task AddProducts(Product product)
        {
            var response = await _httpClient.PostAsync($"api/v1/products", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }


        public async Task UpdateProduct(int productID, Product product)
        {
            var response = await _httpClient.PutAsync($"api/v1/products/{productID}", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DeleteProducts(int productID)
        {
            var response = await _httpClient.DeleteAsync($"api/v1/products/{productID}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

    }

    public interface IProductService
    {
        Task<List<Product>?> GetProducts();
        Task AddProducts(Product product);
        Task UpdateProduct(int productID, Product product);
        Task DeleteProducts(int productID);

    }
}
