using Snackis.Domain.Entities;
using Snackis.Application.DTOs;
using System.Net.Http.Json;
using Snackis.Application.Interfaces.Api;

namespace Snackis.Application.Services.Api
{
    public class PostServiceApi : IPostServiceApi
    {
        private readonly HttpClient _httpClient;

        public PostServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationPost MapToApplication(PostDTO d) => new()
        {
            Id = d.Id,
            Content = d.Content,
            CreatedAt = d.CreatedAt,
            TopicId = d.TopicId,
            UserId = d.UserId,
            AuthorName = d.AuthorName
        };

        public async Task<List<ApplicationPost>> GetAllPostsAsync(int topicId)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<PostDTO>>($"api/post/topic/{topicId}");
            if (dtos == null) return new List<ApplicationPost>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationPost?> GetPostByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<PostDTO>($"api/post/{id}");
            if (dto == null) return null;

            return MapToApplication(dto);
        }

        public async Task CreatePostAsync(ApplicationPost post)
        {
            var dto = new CreatePostDTO
            {
                Content = post.Content,
                TopicId = post.TopicId,
                UserId = post.UserId
            };

            var response = await _httpClient.PostAsJsonAsync("api/post", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePostAsync(ApplicationPost post)
        {
            var dto = new PostDTO
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                TopicId = post.TopicId,
                UserId = post.UserId,
                AuthorName = post.AuthorName
            };

            var response = await _httpClient.PutAsJsonAsync($"api/post/{post.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/post/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}