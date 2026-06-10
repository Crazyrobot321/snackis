using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snackis.Application.Services
{
    public class PrivateMessageService : IPrivateMessageService
    {
        private readonly IPrivateMessageRepository _repo;

        public PrivateMessageService(IPrivateMessageRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

            return await _repo.GetInboxAsync(userId);
        }

        public async Task<ApplicationPrivateMessage?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid message ID: {id}");

            return await _repo.GetByIdAsync(id);
        }
        public async Task<List<ApplicationPrivateMessage>> GetSentMessagesAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            return await _repo.GetSentAsync(userId);
        }

        public async Task AddAsync(ApplicationPrivateMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("Message content cannot be empty");

            if (string.IsNullOrWhiteSpace(message.SenderId) || string.IsNullOrWhiteSpace(message.ReceiverId))
                throw new ArgumentException("Message must have both a sender and a receiver");

            await _repo.AddAsync(message);
        }

        public async Task UpdateAsync(ApplicationPrivateMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (message.Id <= 0)
                throw new ArgumentException("Invalid message ID");

            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("Message content cannot be empty");

            await _repo.UpdateAsync(message);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid message ID");

            await _repo.DeleteAsync(id);
        }
    }
}