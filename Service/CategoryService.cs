using CursoBlazorWebAssembly.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CursoBlazorWebAssembly.Service
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _serializerOptions;



        public CategoryService(HttpClient httpClient, JsonSerializerOptions serializerOptions)
        {
            _httpClient = httpClient;
            _serializerOptions = serializerOptions;
        }



        public async Task<List<Category>?> GetCategories() 
        {
            var response = await _httpClient.GetAsync("v1/categories");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<Category>>(content,_serializerOptions);
        }

        public async Task AddCategory(Category category)
        {
            var response = await _httpClient.PostAsync("v1/categories", JsonContent.Create(category));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DeleteCategory(int categoryId)
        {
            var response = await _httpClient.DeleteAsync($"v1/categories/{categoryId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
    }
}
