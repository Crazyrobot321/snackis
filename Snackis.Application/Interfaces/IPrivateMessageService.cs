using Snackis.Domain.Entities;

namespace Snackis.Application.Interfaces
{
    public interface IPrivateMessageService
    {
        Task AddAsync(ApplicationPrivateMessage message);
        Task DeleteAsync(int id);
        Task<ApplicationPrivateMessage?> GetByIdAsync(int id);
        Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId);
        Task<List<ApplicationPrivateMessage>> GetSentMessagesAsync(string userId);
            Task UpdateAsync(ApplicationPrivateMessage message);
    }
}