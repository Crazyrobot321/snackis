using Snackis.Domain.Entities;
using Snackis.Application.DTOs;
using System.Net.Http.Json;
using Snackis.Application.Interfaces.Api;

namespace Snackis.Application.Services.Api
{
    public class SubCategoryServiceApi : ISubCategoryServiceApi
    {
        private readonly HttpClient _httpClient;

        public SubCategoryServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationTopic MapTopic(TopicDTO t) => new()
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Created = t.Created,
            SubCategoryId = t.SubCategoryId
        };

        private static ApplicationSubCategory MapToApplication(SubCategoryDTO d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            CategoryId = d.CategoryId,
            Topics = d.Topics?.Select(MapTopic).ToList() ?? new List<ApplicationTopic>()
        };

        public async Task<List<ApplicationSubCategory>> GetAllAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<SubCategoryDTO>>("api/subcategory");
            if (dtos == null) return new List<ApplicationSubCategory>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationSubCategory?> GetByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<SubCategoryDTO>($"api/subcategory/{id}");
            if (dto == null) return null;

            return MapToApplication(dto);
        }

        public async Task<List<ApplicationSubCategory>> GetByCategoryIdAsync(int categoryId)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<SubCategoryDTO>>($"api/subcategory/category/{categoryId}");
            if (dtos == null) return new List<ApplicationSubCategory>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task CreateAsync(ApplicationSubCategory subCategory)
        {
            var dto = new CreateSubCategoryDTO
            {
                Name = subCategory.Name,
                Description = subCategory.Description,
                CategoryId = subCategory.CategoryId
            };

            var response = await _httpClient.PostAsJsonAsync("api/subcategory", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(ApplicationSubCategory subCategory)
        {
            var dto = new CreateSubCategoryDTO
            {
                Name = subCategory.Name,
                Description = subCategory.Description,
                CategoryId = subCategory.CategoryId
            };

            var response = await _httpClient.PutAsJsonAsync($"api/subcategory/{subCategory.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/subcategory/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}