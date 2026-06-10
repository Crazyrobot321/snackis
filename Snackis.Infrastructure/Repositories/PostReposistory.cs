using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.Data;
using Snackis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class PostReposistory : IPostReposistory
    {
        private readonly MyDbContext _myDbContext;

        public PostReposistory(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task CreateAsync(ApplicationPost post)
        {
            _myDbContext.Posts.Add(post);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _myDbContext.Posts
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }
        public async Task UpdateAsync(ApplicationPost post)
        {
            _myDbContext.Posts.Update(post);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task<ApplicationPost?> GetByIdAsync(int id)
        {
            return await _myDbContext.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<ApplicationPost>> GetAllAsync()
        {
            return await _myDbContext.Posts
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<List<ApplicationPost>> GetAllByTopicAsync(int id)
        {
            return await _myDbContext.Posts
                .Where(t => t.TopicId == id)
                .Include(u => u.User)
                .ToListAsync();
        }
    }
}
