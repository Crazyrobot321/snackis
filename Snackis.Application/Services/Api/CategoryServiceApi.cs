using Snackis.Domain.Entities;
using Snackis.Application.DTOs;
using System.Net.Http.Json;
using Snackis.Application.Interfaces.Api;

namespace Snackis.Application.Services.Api
{
    public class CategoryServiceApi : ICategoryServiceApi
    {
        private readonly HttpClient _httpClient;

        public CategoryServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationCategory MapToApplication(CategoryDTO d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        };

        public async Task<List<ApplicationCategory>> GetAllCategoriesAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("api/category");
            if (dtos == null) 
                return new List<ApplicationCategory>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationCategory?> GetCategoryByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<CategoryDTO>($"api/category/{id}");
            if (dto == null) 
                return null;

            return MapToApplication(dto);
        }

        public async Task CreateCategoryAsync(ApplicationCategory category)
        {
            var dto = new CreateCategoryDTO
            {
                Name = category.Name,
                Description = category.Description
            };

            var response = await _httpClient.PostAsJsonAsync("api/category", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCategoryAsync(ApplicationCategory category)
        {
            var dto = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            var response = await _httpClient.PutAsJsonAsync($"api/category/{category.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/category/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}