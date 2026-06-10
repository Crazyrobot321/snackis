using Snackis.Domain.Entities;

namespace Snackis.Infrastructure.Repositories
{
    public interface IPrivateMessageRepository
    {
        Task AddAsync(ApplicationPrivateMessage message);
        Task DeleteAsync(int id);
        Task<ApplicationPrivateMessage?> GetByIdAsync(int id);
        Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId);
        Task<List<ApplicationPrivateMessage>> GetSentAsync(string userId);
        Task UpdateAsync(ApplicationPrivateMessage message);
    }
}