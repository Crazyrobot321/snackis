using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Snackis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Interfaces;

namespace Snackis.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _myDbContext;

        public CategoryRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task CreateAsync(ApplicationCategory category)
        {
            _myDbContext.Categories.Add(category);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _myDbContext.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.Topics)
                        .ThenInclude(t => t.Posts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                _myDbContext.Categories.Remove(category);
                await _myDbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(ApplicationCategory category)
        {
            _myDbContext.Categories.Update(category);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task<List<ApplicationCategory>> GetAll()
        {
            return await _myDbContext.Categories.ToListAsync();
        }
        public async Task<ApplicationCategory?> GetByIdAsync(int id)
        {
            return await _myDbContext.Categories.FindAsync(id);
        }
    }
}
