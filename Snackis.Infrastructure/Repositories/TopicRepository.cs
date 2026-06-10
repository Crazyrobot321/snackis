using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Data;
using Snackis.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snackis.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly MyDbContext _myDbContext;

        public TopicRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task CreateAsync(ApplicationTopic topic)
        {
            _myDbContext.Topics.Add(topic);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = await _myDbContext.Topics
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topic != null)
            {
                _myDbContext.Topics.Remove(topic);
                await _myDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ApplicationTopic topic)
        {
            _myDbContext.Topics.Update(topic);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationTopic>> GetAllAsync()
        {
            return await _myDbContext.Topics
                .Include(t => t.Author)
                .Include(t => t.Posts)
                .ToListAsync();
        }
        public async Task<List<ApplicationTopic>> GetAllTopicsBySubId(int subCategoryId)
        {
            return await _myDbContext.Topics
                .Where(t => t.SubCategoryId == subCategoryId)
                .Include(t => t.Author)
                .Include(t => t.Posts)
                .ToListAsync();
        }
        public async Task<ApplicationTopic?> GetByIdAsync(int id)
        {
            return await _myDbContext.Topics
                .Include(t => t.Author)
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<ApplicationPost>> GetPostsByTopicIdAsync(int id)
        {
            return await _myDbContext.Posts
                .Where(p => p.TopicId == id)
                .ToListAsync();
        }
    }
}
