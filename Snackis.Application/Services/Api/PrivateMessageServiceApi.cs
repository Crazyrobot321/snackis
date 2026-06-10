using System.Net.Http.Json;
using Snackis.Application.DTOs;
using Snackis.Application.Interfaces.Api;
using Snackis.Domain.Entities;

namespace Snackis.Application.Services.Api
{
    public class PrivateMessageServiceApi : IPrivateMessageServiceApi
    {
        private readonly HttpClient _httpClient;

        public PrivateMessageServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationPrivateMessage MapToApplication(PrivateMessageDTO d) => new()
        {
            Id = d.Id,
            SenderId = d.SenderId,
            ReceiverId = d.ReceiverId,
            Title = d.Title,
            Content = d.Content,
            SentAt = d.SentAt,
            SenderName = d.SenderName
        };

        public async Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<PrivateMessageDTO>>($"api/PrivateMessages/inbox/{userId}");
            if (dtos == null) return new List<ApplicationPrivateMessage>();
            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationPrivateMessage?> GetByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<PrivateMessageDTO>($"api/PrivateMessages/{id}");
            if (dto == null) return null;
            return MapToApplication(dto);
        }

        public async Task<List<ApplicationPrivateMessage>> GetSentAsync(string userId)
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<PrivateMessageDTO>>($"api/PrivateMessages/sent/{userId}");
            if (dtos == null) return new List<ApplicationPrivateMessage>();
            return dtos.Select(MapToApplication).ToList();
        }

        public async Task SendMessageAsync(string senderId, string recipientId, string subject, string content)
        {
            var dto = new CreatePrivateMessageDTO
            {
                SenderId = senderId,
                ReceiverId = recipientId,
                Title = subject,
                Content = content,
                SentAt = DateTime.Now
            };
            var response = await _httpClient.PostAsJsonAsync("api/PrivateMessages", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(ApplicationPrivateMessage message)
        {
            var dto = new PrivateMessageDTO
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Title = message.Title,
                Content = message.Content,
                SentAt = message.SentAt
            };
            var response = await _httpClient.PutAsJsonAsync($"api/PrivateMessages/{message.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/PrivateMessages/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}