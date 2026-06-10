using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis.Infrastructure.Repositories
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        private readonly MyDbContext _myDbContext;

        public PrivateMessageRepository(MyDbContext context)
        {
            _myDbContext = context;
        }

        public async Task<List<ApplicationPrivateMessage>> GetInboxAsync(string userId)
        {
            return await _myDbContext.privateMessages
                .Include(m => m.Sender)
                .Where(m => m.ReceiverId == userId)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }


        public async Task<ApplicationPrivateMessage?> GetByIdAsync(int id)
        {
            return await _myDbContext.privateMessages.FindAsync(id);
        }
        public async Task<List<ApplicationPrivateMessage>> GetSentAsync(string userId)
        {
            return await _myDbContext.privateMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == userId)
                .ToListAsync();
        }
        public async Task AddAsync(ApplicationPrivateMessage message)
        {
            await _myDbContext.privateMessages.AddAsync(message);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationPrivateMessage message)
        {
            _myDbContext.privateMessages.Update(message);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _myDbContext.privateMessages.FindAsync(id);
            if (message != null)
            {
                _myDbContext.privateMessages.Remove(message);
                await _myDbContext.SaveChangesAsync();
            }
        }
    }
}