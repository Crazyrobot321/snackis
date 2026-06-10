using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces.Api
{
    public interface IPrivateMessageServiceApi
    {
        Task DeleteAsync(int id);
        Task<ApplicationPrivateMessage?> GetByIdAsync(int id);
        Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId);
        Task<List<ApplicationPrivateMessage>> GetSentAsync(string userId);
        Task SendMessageAsync(string senderId, string recipientId, string subject, string content);
        Task UpdateAsync(ApplicationPrivateMessage message);
    }
}