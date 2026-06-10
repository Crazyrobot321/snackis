using Snackis.Domain.Entities;
using Snackis.Application.DTOs;
using System.Net.Http.Json;
using Snackis.Application.Interfaces.Api;

namespace Snackis.Application.Services.Api
{
    public class TopicServiceApi : ITopicServiceApi
    {
        private readonly HttpClient _httpClient;

        public TopicServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationTopic MapToApplication(TopicDTO d) => new()
        {
            Id = d.Id,
            Title = d.Title,
            Description = d.Description,
            Created = d.Created,
            AuthorId = d.UserId,
            AuthorName = d.AuthorName,
            ImageSource = d.ImageSource,
            PostCount = d.PostCount
        };

        private static ApplicationPost MapPost(PostDTO p) => new()
        {
            Id = p.Id,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            TopicId = p.TopicId,
            UserId = p.UserId,
            AuthorName = p.AuthorName
        };

        public async Task<List<ApplicationTopic>> GetAllTopicsAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<TopicDTO>>("api/topic");
            if (dtos == null) return new List<ApplicationTopic>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<List<ApplicationTopic>> GetAllTopicsBySubId(int subCategoryId)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<TopicDTO>>($"api/topic/subcategory/{subCategoryId}");
            if (dtos == null)
                return new List<ApplicationTopic>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationTopic?> GetTopicByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<TopicDTO>($"api/topic/{id}");
            if (dto == null) return null;

            return MapToApplication(dto);
        }

        public async Task<List<ApplicationPost>> GetPostsByTopicIdAsync(int id)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<PostDTO>>($"api/topic/{id}/posts");
            if (dtos == null) return new List<ApplicationPost>();

            return dtos.Select(MapPost).ToList();
        }

        public async Task CreateTopicAsync(ApplicationTopic topic)
        {
            var dto = new CreateTopicDTO
            {
                Title = topic.Title,
                Description = topic.Description,
                CategoryId = topic.CategoryId,
                SubCategoryId = topic.SubCategoryId,
                AuthorId = topic.AuthorId,
                Created = topic.Created,
                ImageSource = topic.ImageSource
            };

            var response = await _httpClient.PostAsJsonAsync("api/topic", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTopicAsync(ApplicationTopic topic)
        {
            var dto = new CreateTopicDTO
            {
                Title = topic.Title,
                Description = topic.Description,
                CategoryId = topic.CategoryId,
                SubCategoryId = topic.SubCategoryId,
                AuthorId = topic.AuthorId,
                Created = topic.Created
            };

            var response = await _httpClient.PutAsJsonAsync($"api/topic/{topic.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTopicAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/topic/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}